using System;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using RatchetEdit.Serializers;
using RatchetEdit.LevelObjects;
using static RatchetEdit.Utilities;
using System.Drawing;

namespace RatchetEdit
{
    public partial class Main : Form
    {
        public Dictionary<int, string> mobNames, tieNames;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            mobNames = GetModelNames("/ModelLists/ModelListRC1.txt");
            tieNames = GetModelNames("/ModelLists/TieModelsRC1.txt");
        }

        private void mapOpenBtn_Click(object sender, EventArgs e)
        {
            if (mapOpenDialog.ShowDialog() == DialogResult.OK)
            {
                LoadLevel(mapOpenDialog.FileName);
            }
        }

        public LevelUserControl GetActiveTab()
        {
            if (tabControl1.SelectedIndex == -1)
            {
                return null;
            }
            return (LevelUserControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
        }

        void LoadLevel(string fileName)
        {
            bool hasNoTabs = tabControl1.SelectedIndex == -1;

            TabPage newPage = new TabPage();
            newPage.Text = fileName.Substring(Math.Max(0, fileName.Length - 20));
            newPage.Controls.Add(new LevelUserControl(this, fileName));

            tabControl1.TabPages.Add(newPage);
            tabControl1.SelectTab(tabControl1.TabCount - 1);

            if (hasNoTabs)
            {
                UpdateMenuButtons();
            }
        }

        private Dictionary<int, string> GetModelNames(string fileName) {
            var modelNames = new Dictionary<int, string>();

            try
            {
                using (StreamReader stream = new StreamReader(Application.StartupPath + fileName))
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] stringPart = line.Split('=');
                        int modelId = int.Parse(stringPart[0], NumberStyles.HexNumber);
                        modelNames.Add(modelId, stringPart[1]);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Model list file not found! No names for you!");
            }

            return modelNames;
        }

        #region Open Viewers
        private void OpenModelViewer()
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.OpenModelViewer();
            }
        }

        public void OpenTextureViewer()
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.OpenTextureViewer();
            }
        }

        public void OpenSpriteViewer()
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.OpenSpriteViewer();
            }
        }

        public void OpenUISpriteViewer()
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.OpenUISpriteViewer();
            }
        }

        public void OpenLanguageViewer()
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.OpenLanguageViewer();
            }
        }
        #endregion

        #region MenuButtons
        private void UISpriteToolBtn_Click(object sender, EventArgs e)
        {
            OpenUISpriteViewer();
        }

        private void spriteViewerToolBtn_Click(object sender, EventArgs e)
        {
            OpenSpriteViewer();
        }

        private void modelViewerToolBtn_Click(object sender, EventArgs e)
        {
            OpenModelViewer();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            OpenTextureViewer();
        }

        private void languageDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLanguageViewer();
        }


        private void exitToolBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion MenuButtons

        //Called every frame
        private void tickTimer_Tick(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.Tick();
            }
        }

        private void mobyCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableMoby = mobyCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void tieCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableTie = tieCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void shrubCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableShrub = shrubCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void collCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableCollision = collCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void terrainCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableTerrain = terrainCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void splineCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableSpline = splineCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void skyboxCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableSkybox = skyboxCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void cuboidCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableCuboid = cuboidCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void type0CCheck_CheckedChanged(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                activeTab.GetGLControl().enableType0C = type0CCheck.Checked;
                activeTab.InvalidateView();
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 4, e.Bounds.Top + 4);
                e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            }
            else
            {
                e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 9, e.Bounds.Top + 4);
            }
            e.DrawFocusRectangle();
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                Rectangle r = tabControl1.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    LevelUserControl activeTab = GetActiveTab();
                    if (activeTab != null)
                    {
                        activeTab.PrepareForClose();
                    }

                    if (i + 1 < tabControl1.TabCount)
                    {
                        tabControl1.SelectedIndex = i + 1;
                    }
                    else if (i - 1 >= 0)
                    {
                        tabControl1.SelectedIndex = i - 1;
                    }
                    tabControl1.TabPages.RemoveAt(i);
                }
            }
        }

        private void UpdateMenuButtons()
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab == null)
            {
                mapSaveBtn.Enabled = false;
                mapSaveAsBtn.Enabled = false;
                foreach (ToolStripMenuItem menuButton in WindowToolStripItem.DropDownItems)
                {
                    menuButton.Enabled = false;
                }
                foreach (ToolStripMenuItem menuButton in ViewToolStipItem.DropDownItems)
                {
                    menuButton.Checked = false;
                    menuButton.Enabled = false;
                }
            }
            else
            {
                // mapSaveBtn.Enabled = true;  // Uncomment this when the save button has an event handler
                mapSaveAsBtn.Enabled = true;

                foreach (ToolStripMenuItem menuButton in WindowToolStripItem.DropDownItems)
                {
                    menuButton.Enabled = true;
                }
                foreach (ToolStripMenuItem menuButton in ViewToolStipItem.DropDownItems)
                {
                    menuButton.Enabled = true;
                }

                CustomGLControl glControl = activeTab.GetGLControl();
                mobyCheck.Checked = glControl.enableMoby;
                tieCheck.Checked = glControl.enableTie;
                shrubCheck.Checked = glControl.enableShrub;
                collCheck.Checked = glControl.enableCollision;
                terrainCheck.Checked = glControl.enableTerrain;
                splineCheck.Checked = glControl.enableSpline;
                skyboxCheck.Checked = glControl.enableSkybox;
                cuboidCheck.Checked = glControl.enableCuboid;
                type0CCheck.Checked = glControl.enableType0C;

                activeTab.InvalidateView();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMenuButtons();
        }

        private void mapSaveAsBtn_Click(object sender, EventArgs e)
        {
            LevelUserControl activeTab = GetActiveTab();
            if (activeTab != null)
            {
                if (mapSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    string pathName = Path.GetDirectoryName(mapSaveDialog.FileName);

                    GameplaySerializer gameplaySerializer = new GameplaySerializer();
                    gameplaySerializer.Save(activeTab.level, mapSaveDialog.FileName);
                    EngineSerializer engineSerializer = new EngineSerializer();
                    engineSerializer.Save(activeTab.level, pathName);
                    Console.WriteLine(pathName);
                }
            }
        }
    }
}
