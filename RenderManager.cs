using System;
using System.IO;
using Gdk;
using GLib;
using Gtk;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RatchetEdit.LevelObjects;
using RatchetEdit.Models;
using RatchetEdit.Tools;
using Key = OpenTK.Input.Key;
using RatchetEdit.UI;

namespace RatchetEdit
{
    public class RenderManager
    {

        public Level level;

        public Matrix4 worldView;
        private Matrix4 projection;
        private Matrix4 view;
        private int width, height;

        public int shaderID, colorShaderID, collisionShaderID;
        public int matrixID, colorID;

        private int currentSplineVertex;
        public LevelObject selectedObject;
        private int VAO;

        private Vector3 prevMouseRay;
        private int lastMouseX, lastMouseY;
        private bool xLock, yLock, zLock, rMouse, lMouse;
        private bool enableTranslateTool, enableRotateTool, enableScaleTool, enableSplineTool;

        public bool initialized, invalidate;
        public bool enableMoby, enableTie, enableShrub, enableSpline,
            enableCuboid, enableType0C, enableSkybox, enableTerrain, enableCollision;

        public Camera camera;
        private Tool currentTool;
        public Tool translateTool, rotationTool, scalingTool, vertexTranslator;

        public event EventHandler<RatchetEventArgs> ObjectClick;
        public event EventHandler<RatchetEventArgs> ObjectDeleted;

        private Gtk.Window parent;
        public readonly ViewportArea ViewportWidget;
        private bool wantsToMove = false;
        private bool mouseInViewport = false;

        public RenderManager(Gtk.Window parent)
        {
            this.parent = parent;

            var graphicsMode = new GraphicsMode
            (
                new ColorFormat(24),
                24, 0, 4, 0, 2,
                false
            );

            this.ViewportWidget = new ViewportArea(graphicsMode, 3, 3, GraphicsContextFlags.Default)
            {
                AutoRender = true,
                CanFocus = true
            };

            this.ViewportWidget.Events |=
                EventMask.ButtonPressMask |
                EventMask.ButtonReleaseMask |
                EventMask.EnterNotifyMask |
                EventMask.LeaveNotifyMask |
                EventMask.KeyPressMask |
                EventMask.KeyReleaseMask;

            this.ViewportWidget.Initialized += this.OnViewportWidgetInitialized;
            this.ViewportWidget.Render += this.OnViewportWidgetRender;
            this.ViewportWidget.Resize += this.OnViewportWidgetResize;

            this.ViewportWidget.ButtonPressEvent += this.OnViewportButtonPressed;
            this.ViewportWidget.ButtonReleaseEvent += this.OnViewportButtonReleased;
            this.ViewportWidget.EnterNotifyEvent += this.OnViewportMouseEnter;
            this.ViewportWidget.LeaveNotifyEvent += this.OnViewportMouseLeave;
        }

        void OnViewportWidgetResize(object sender, EventArgs args)
        {
            if (!initialized) return;
            width = this.ViewportWidget.AllocatedWidth;
            height = this.ViewportWidget.AllocatedHeight;
            GL.Viewport(0, 0, width, height);
            projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3, (float)width / height, 0.1f, 800.0f);
            InvalidateView();
        }

        void OnViewportWidgetInitialized(object sender, EventArgs args)
        {
            ViewportWidget.MakeCurrent();

            GL.GenVertexArrays(1, out VAO);
            GL.BindVertexArray(VAO);

            //Setup openGL variables
            GL.ClearColor(0.53f, 0.81f, 0.92f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.LineWidth(5.0f);

            //Setup general shader
            shaderID = GL.CreateProgram();
            LoadShader("Shaders/vs.glsl", ShaderType.VertexShader, shaderID);
            LoadShader("Shaders/fs.glsl", ShaderType.FragmentShader, shaderID);
            GL.LinkProgram(shaderID);

            //Setup color shader
            colorShaderID = GL.CreateProgram();
            LoadShader("Shaders/colorshadervs.glsl", ShaderType.VertexShader, colorShaderID);
            LoadShader("Shaders/colorshaderfs.glsl", ShaderType.FragmentShader, colorShaderID);
            GL.LinkProgram(colorShaderID);

            //Setup color shader
            collisionShaderID = GL.CreateProgram();
            LoadShader("Shaders/collisionshadervs.glsl", ShaderType.VertexShader, collisionShaderID);
            LoadShader("Shaders/collisionshaderfs.glsl", ShaderType.FragmentShader, collisionShaderID);
            GL.LinkProgram(collisionShaderID);

            matrixID = GL.GetUniformLocation(shaderID, "MVP");
            colorID = GL.GetUniformLocation(colorShaderID, "incolor");

            width = this.ViewportWidget.AllocatedWidth;
            height = this.ViewportWidget.AllocatedHeight;
            GL.Viewport(0, 0, width, height);
            projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3, (float)width / height, 0.1f, 800.0f);

            camera = new Camera();

            translateTool = new TranslationTool();
            rotationTool = new RotationTool();
            scalingTool = new ScalingTool();
            vertexTranslator = new VertexTranslationTool();

            initialized = true;
            InvalidateView();
        }

        void OnViewportWidgetRender(object sender, EventArgs args)
        {
            this.ViewportWidget.MakeCurrent();

            this.TickBeforeRender();
            this.InnerRender();

            this.ViewportWidget.QueueRender();
        }

        void TickBeforeRender()
        {
            MouseState state = Mouse.GetCursorState();

            float deltaTime = 0.016f;

            KeyboardState keyState = OpenTK.Input.Keyboard.GetState();
            float moveSpeed = keyState.IsKeyDown(Key.LShift) ? 40 : 10;

            if (mouseInViewport && state.IsButtonDown(MouseButton.Right))
            {
                camera.rotation.Z -= (state.X - lastMouseX) * camera.speed * 0.016f;
                camera.rotation.X -= (state.Y - lastMouseY) * camera.speed * 0.016f;
                camera.rotation.X = MathHelper.Clamp(camera.rotation.X, MathHelper.DegreesToRadians(-89.9f), MathHelper.DegreesToRadians(89.9f));
                InvalidateView();
            }

            Vector3 moveDir = GetInputAxes();
            if (moveDir.Length > 0)
            {
                moveDir *= moveSpeed * deltaTime;
                InvalidateView();
            }
            camera.Translate(Vector3.Transform(moveDir, camera.GetRotationMatrix()));

            view = camera.GetViewMatrix();

            Vector3 mouseRay = Utilities.MouseToWorldRay(projection, view, new System.Drawing.Size(width, height), new Vector2(state.X, state.Y));
            bool toolIsBeingDragged = xLock || yLock || zLock;
            if (toolIsBeingDragged)
            {
                Vector3 direction = Vector3.Zero;
                if (xLock) direction = Vector3.UnitX;
                else if (yLock) direction = Vector3.UnitY;
                else if (zLock) direction = Vector3.UnitZ;
                float magnitudeMultiplier = 20;
                Vector3 magnitude = (mouseRay - prevMouseRay) * magnitudeMultiplier;


                switch (currentTool)
                {
                    case TranslationTool t:
                        selectedObject.Translate(direction * magnitude);
                        break;
                    case RotationTool t:
                        selectedObject.Rotate(direction * magnitude);
                        break;
                    case ScalingTool t:
                        selectedObject.Scale(direction * magnitude + Vector3.One);
                        break;
                    case VertexTranslationTool t:
                        if (selectedObject is Spline spline)
                        {
                            spline.TranslateVertex(currentSplineVertex, direction * magnitude);
                        }
                        break;
                }

                InvalidateView();
            }

            prevMouseRay = mouseRay;
            lastMouseX = state.X;
            lastMouseY = state.Y;
        }

        void InnerRender()
        {
            if (!invalidate) return;
            invalidate = false;

            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            if (this.level == null) return;

            worldView = view * projection;

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            ViewportWidget.MakeCurrent();

            GL.UseProgram(shaderID);

            if (enableMoby)
                foreach (Moby mob in level.mobs)
                    mob.Render(this, mob == selectedObject);

            if (enableTie)
                foreach (Tie tie in level.ties)
                    tie.Render(this, tie == selectedObject);

            if (enableShrub)
                foreach (Shrub shrub in level.shrubs)
                    shrub.Render(this, shrub == selectedObject);

            if (enableSpline)
                foreach (Spline spline in level.splines)
                    spline.Render(this, spline == selectedObject);

            if (enableTerrain)
                foreach (TerrainModel tFrag in level.terrains)
                    tFrag.Draw(this);

            if (enableSkybox)
                level.skybox.Draw(this);

            GL.UseProgram(colorShaderID);

            if (enableCuboid)
                foreach (Cuboid cuboid in level.cuboids)
                    cuboid.Render(this, cuboid == selectedObject);

            if (enableType0C)
                foreach (Type0C cuboid in level.type0Cs)
                    cuboid.Render(this, cuboid == selectedObject);

            if (enableCollision)
            {
                Collision col = (Collision)level.collisionModel;
                col.DrawCol(this);
            }

            //RenderTool();

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
        }


        void LoadShader(string filename, ShaderType type, int program)
        {
            int address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public void LoadLevel(Level level)
        {
            this.level = level;
            enableMoby = true;
            enableTie = true;
            enableShrub = true;
            enableTerrain = true;

            Moby ratchet = level.mobs[0];
            camera.MoveBehind(ratchet);

            SelectObject(null);
        }

        public void SelectObject(LevelObject newObject = null)
        {
            if (newObject == null)
            {
                selectedObject = null;
                InvalidateView();
                return;
            }

            if ((selectedObject is Spline) && !(newObject is Spline))
            {
                //Previous object was spline, new isn't
                if (currentTool is VertexTranslationTool) SelectTool(null);
            }

            selectedObject = newObject;

            ObjectClick?.Invoke(this, new RatchetEventArgs
            {
                Object = newObject
            });

            InvalidateView();
        }

        public void SelectTool(Tool tool = null)
        {
            enableTranslateTool = (tool is TranslationTool);
            enableRotateTool = (tool is RotationTool);
            enableScaleTool = (tool is ScalingTool);
            enableSplineTool = (tool is VertexTranslationTool);
            currentTool = tool;

            currentSplineVertex = 0;
            InvalidateView();
        }

        void InvalidateView()
        {
            invalidate = true;
        }

        private Vector3 GetInputAxes()
        {
            KeyboardState keyState = OpenTK.Input.Keyboard.GetState();

            float xAxis = 0, yAxis = 0, zAxis = 0;

            if (this.mouseInViewport)
            {
                if (keyState.IsKeyDown(Key.W)) yAxis++;
                if (keyState.IsKeyDown(Key.S)) yAxis--;
                if (keyState.IsKeyDown(Key.A)) xAxis--;
                if (keyState.IsKeyDown(Key.D)) xAxis++;
                if (keyState.IsKeyDown(Key.Q)) zAxis--;
                if (keyState.IsKeyDown(Key.E)) zAxis++;
            }


            return new Vector3(xAxis, yAxis, zAxis);
        }

        /// <summary>
        /// Handles input inside the OpenGL viewport for mouse button presses.
        /// This function grabs focus for the viewport, and hides the mouse
        /// cursor during movement.
        /// </summary>
        [ConnectBefore]
        private void OnViewportButtonPressed(object o, ButtonPressEventArgs args)
        {
            if (args.Event.Type != EventType.ButtonPress)
            {
                return;
            }

            bool validButtonIsPressed = false;
                // Allow both right and left
                if (args.Event.Button == 1 || args.Event.Button == 3)
                {
                    validButtonIsPressed = true;
                }

            if (!validButtonIsPressed)
            {
                return;
            }

            this.ViewportWidget.GrabFocus();
            wantsToMove = true;
        }

        /// <summary>
        /// Handles input inside the OpenGL viewport for mouse button releases.
        /// This function restores input focus to the main UI and returns the
        /// cursor to its original appearance.
        /// </summary>
        [ConnectBefore]
        private void OnViewportButtonReleased(object o, ButtonReleaseEventArgs args)
        {
            if (args.Event.Type != EventType.ButtonRelease)
            {
                return;
            }

            bool validButtonIsPressed = false;
                // Allow both right and left
                if (args.Event.Button == 1 || args.Event.Button == 3)
                {
                    validButtonIsPressed = true;
                }

            if (!validButtonIsPressed)
            {
                return;
            }

            wantsToMove = false;
        }

        /// <summary>
        /// Handles changing the cursor when leaving the viewport.
        /// </summary>
        /// <param name="o">The sending object.</param>
        /// <param name="args">The event arguments.</param>
        [ConnectBefore]
        private void OnViewportMouseLeave(object o, LeaveNotifyEventArgs args)
        {
            mouseInViewport = false;
        }

        /// <summary>
        /// Handles changing the cursor when hovering over the viewport.
        /// </summary>
        /// <param name="o">The sending object.</param>
        /// <param name="args">The event arguments.</param>
        [ConnectBefore]
        private void OnViewportMouseEnter(object o, EnterNotifyEventArgs args)
        {
            mouseInViewport = true;
        }

        public void Dispose()
        {
            GL.DeleteVertexArrays(1, ref VAO);
        }
    }

}