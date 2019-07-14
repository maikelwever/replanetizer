using System;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using RatchetEdit.Serializers;
using RatchetEdit.LevelObjects;
using static RatchetEdit.Utilities;

namespace RatchetEdit
{
    public partial class LevelUserControl : UserControl
    {
        Main parent;
        String fileName;

        public Level level;
        public ModelViewer modelViewer;
        public TextureViewer textureViewer;
        public SpriteViewer spriteViewer;
        public UIViewer uiViewer;
        public LanguageViewer languageViewer;

        bool suppressTreeViewSelectEvent = false;

        public LevelUserControl(Main parent, String fileName)
        {
            this.parent = parent;
            this.fileName = fileName;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            glControl.SelectTool(glControl.translateTool);
            objectTree.Init(parent.mobNames, parent.tieNames);

            Level level = new Level(fileName);
            if (!level.valid) return;
            this.level = level;

            glControl.LoadLevel(level);
            objectTree.UpdateEntries(level);
            UpdateProperties(null);
        }

        public LevelObject GetSelectedObject()
        {
            return glControl.selectedObject;
        }

        public void UpdateProperties(LevelObject obj)
        {
            properties.SelectedObject = obj;
            properties.Refresh();
        }

        public void PrepareForClose()
        {
            if (modelViewer != null)
            {
                modelViewer.Close();
            }
            if (textureViewer != null)
            {
                textureViewer.Close();
            }
            if (spriteViewer != null)
            {
                spriteViewer.Close();
            }
            if (uiViewer != null)
            {
                uiViewer.Close();
            }
            if (languageViewer != null)
            {
                languageViewer.Close();
            }
        }

        #region Open Viewers
        public void OpenModelViewer()
        {
            if ((modelViewer == null || modelViewer.IsDisposed))
            {
                if((GetSelectedObject() is ModelObject modelObj))
                {
                    modelViewer = new ModelViewer(this, modelObj.model);
                    modelViewer.Show();
                }
            }
            else
            {
                if((GetSelectedObject() is ModelObject modelObj))
                {
                    modelViewer.SelectModel(modelObj.model);
                }
                modelViewer.BringToFront();
            }
        }

        public void OpenTextureViewer()
        {
            if (textureViewer == null || textureViewer.IsDisposed)
            {
                textureViewer = new TextureViewer(this);
                textureViewer.Show();
            }
            else
            {
                textureViewer.BringToFront();
            }
        }

        public void OpenSpriteViewer()
        {
            if (spriteViewer == null || spriteViewer.IsDisposed)
            {
                spriteViewer = new SpriteViewer(this);
                spriteViewer.Show();
            }
            else
            {
                spriteViewer.BringToFront();
            }
        }

        public void OpenUISpriteViewer()
        {
            if (uiViewer == null || uiViewer.IsDisposed)
            {
                uiViewer = new UIViewer(this);
                uiViewer.Show();
            }
            else
            {
                uiViewer.BringToFront();
            }
        }

        public void OpenLanguageViewer()
        {
            if (languageViewer == null || languageViewer.IsDisposed)
            {
                languageViewer = new LanguageViewer(this);
                languageViewer.Show();
            }
            else
            {
                languageViewer.BringToFront();
            }
        }
        #endregion

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!glControl.initialized) return;

            //Update ui label texts
            camXLabel.Text = String.Format("X: {0}", fRound(glControl.camera.position.X, 2).ToString());
            camYLabel.Text = String.Format("Y: {0}", fRound(glControl.camera.position.Y, 2).ToString());
            camZLabel.Text = String.Format("Z: {0}", fRound(glControl.camera.position.Z, 2).ToString());
            pitchLabel.Text = String.Format("Pitch: {0}", fRound(fToDegrees(glControl.camera.rotation.X), 2).ToString());
            yawLabel.Text = String.Format("Yaw: {0}", fRound(fToDegrees(glControl.camera.rotation.Z), 2).ToString());
        }

        //Called every frame by the parent window
        public void Tick()
        {
            glControl.Tick();
        }

        private void cloneBtn_Click(object sender, EventArgs e)
        {
            if (!(GetSelectedObject() is Moby moby)) return;
            glControl.CloneMoby(moby);
        }

        public int GetShaderID()
        {
            return glControl.shaderID;
        }

        public void InvalidateView()
        {
            glControl.invalidate = true;
        }

        private void EnableCheck(object sender, EventArgs e)
        {

        }

        private void translateToolBtn_Click(object sender, EventArgs e)
        {
            glControl.SelectTool(glControl.translateTool);
        }

        private void rotateToolBtn_Click(object sender, EventArgs e)
        {
            glControl.SelectTool(glControl.rotationTool);
        }

        private void scaleToolBtn_Click(object sender, EventArgs e)
        {
            glControl.SelectTool(glControl.scalingTool);
        }

        private void splineToolBtn_Click(object sender, EventArgs e)
        {
            glControl.SelectTool(glControl.vertexTranslator);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            glControl.DeleteObject(GetSelectedObject());
        }


        private void objectTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (suppressTreeViewSelectEvent)
            {
                suppressTreeViewSelectEvent = false;
                return;
            }

            if (e.Node.Parent == null) return;
            

            if (e.Node.Parent == objectTree.splineNode)
            {
                glControl.SelectObject(level.splines[e.Node.Index]);
            }
            else if (e.Node.Parent == objectTree.cameraNode)
            {
                glControl.SelectObject(level.gameCameras[e.Node.Index]);
            }
            else if (e.Node.Parent == objectTree.cuboidNode)
            {
                glControl.SelectObject(level.cuboids[e.Node.Index]);
            }
            else if (e.Node.Parent == objectTree.type0CNode)
            {
                glControl.SelectObject(level.type0Cs[e.Node.Index]);
            }

            if (e.Node.Parent.Parent == null) return;

            if (e.Node.Parent.Parent == objectTree.mobyNode)
            {
                glControl.SelectObject(level.mobs[(int)e.Node.Tag]);
            }
            else if (e.Node.Parent.Parent == objectTree.tieNode)
            {
                glControl.SelectObject(level.ties[(int)e.Node.Tag]);
            }
            else if (e.Node.Parent == objectTree.shrubNode)
            {
                glControl.SelectObject(level.shrubs[e.Node.Index]);
            }

            glControl.camera.MoveBehind(GetSelectedObject());
        }


        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            InvalidateView();
        }

        public CustomGLControl GetGLControl()
        {
            return glControl;
        }

        private void glControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            OpenModelViewer();
        }

        private void glControl_ObjectClick(object sender, RatchetEventArgs e)
        {
            UpdateProperties(e.Object);
        }

        private void glControl_ObjectDeleted(object sender, RatchetEventArgs e)
        {
            switch (e.Object)
            {
                case Moby moby:
                    objectTree.mobyNode.Nodes[level.mobs.IndexOf(moby)].Remove();
                    level.mobs.Remove(moby);
                    break;
                case Tie tie:
                    objectTree.tieNode.Nodes[level.ties.IndexOf(tie)].Remove();
                    level.ties.Remove(tie);
                    break;
                case Shrub shrub:
                    level.shrubs.Remove(shrub);
                    break;
            }
            UpdateProperties(e.Object);
        }
    }
}
