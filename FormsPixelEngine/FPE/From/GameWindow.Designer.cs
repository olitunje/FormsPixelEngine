using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering;
using FormsPixelEngine.FPE.Scene;
using System.ComponentModel;
using System.Diagnostics;

namespace FormsPixelEngine.FPE.From
{
    /*! \class GameWindow
    \brief abstract GameWindow class

    This class is the center of the FPE. When inherited it gives access to Rendering, Input, and other systems. The class that inherits GameWindow must also be a MS Form.
    */
    abstract partial class GameWindow
    {
        private System.ComponentModel.IContainer components = null;
        private Stopwatch deltaTimeTracker;

        protected Renderer renderer; /*!< Instance of the Render class for use in Rendering to the current Window \sa Renderer */
        protected InputSystem input;  /*!< Instance of the InputSystem class for use in getting Input for the current window \sa InputSystem */
        protected SceneManager sceneManager; /*!< Instance of the SceneManager class for use in managing scenes \sa SceneManager */

        protected float deltaTime; //!< Times used to process last Frame. 

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent(string _title, uint _windowWidth, uint _windowHeight, uint _pixelWidth, uint _pixelHeight) {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size((int)_windowWidth, (int)_windowHeight);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.DoubleBuffered = true;
            this.Text = _title;

            renderer = new Renderer(_pixelWidth, _pixelHeight);
            input = new InputSystem();
            sceneManager = new SceneManager(renderer, input);
            deltaTimeTracker = new Stopwatch();

            OnInit();

            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 10;
            timer1.Tick += new System.EventHandler((object sender, EventArgs e) => {
                deltaTime = (float)deltaTimeTracker.ElapsedMilliseconds/1000f;
                deltaTimeTracker.Restart();

                sceneManager.currentScene.UpdateScene(deltaTime);
                OnUpdate();
                this.Refresh();
            });

            deltaTimeTracker.Start();
            timer1.Start();
        }


        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Bitmap bitmap = new Bitmap(width: (int)renderer.pixelWidth, height: (int)renderer.pixelHeight, stride: (int)renderer.pixelWidth * sizeof(uint),
                format: System.Drawing.Imaging.PixelFormat.Format32bppPArgb, scan0: renderer.GetPixelBufferPtr());

            Graphics gfx = e.Graphics;

            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gfx.DrawImage(bitmap, -1, 0, this.Width, this.Height);
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);

            sceneManager.currentScene.Close();
            OnClose();
            renderer.Destroy();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            input.UpdateKey((uint)e.KeyValue, true);
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            input.UpdateKey((uint)e.KeyValue, false);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            input.UpdateMousePosition((uint)e.X, (uint)e.Y);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            ResolveMouseButton(e.Button, false);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseDown(e);
            ResolveMouseButton(e.Button, true);
        }
 
        private void ResolveMouseButton(MouseButtons _winButton, bool _state) {
            MouseButton? _button = null;

            switch (_winButton) {
                case MouseButtons.Left: _button = MouseButton.Left; break;
                case MouseButtons.Right: _button = MouseButton.Right; break;
                case MouseButtons.Middle: _button = MouseButton.Middle; break;
                case MouseButtons.XButton1: _button = MouseButton.Button4; break;
                case MouseButtons.XButton2: _button = MouseButton.Button5; break;
            }

            if (_button == null) return;
            input.UpdateMouseButtons((uint)_button, _state);
        }

        //! Abstract Method that is called on Window Initialization
        /*!
          \sa OnUpdate(), OnClose()
        */
        public abstract void OnInit();

        //! Abstract Method that is called when the Window is Updated
        /*!
          \sa OnInit(), OnClose()
        */
        public abstract void OnUpdate();

        //! Abstract Method that is called when the Window is Closed
        /*!
          \sa OnInit(), OnUpdate()
        */
        public abstract void OnClose();
    }
}