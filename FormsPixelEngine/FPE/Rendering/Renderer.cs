using FormsPixelEngine.FPE.From;
using FormsPixelEngine.FPE.Rendering.Sprites;
using FormsPixelEngine.FPE.Rendering.Text;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace FormsPixelEngine.FPE.Rendering
{

    /*! \class Renderer
    \brief Renders to the Window

    This class allows rendering to the window. It provides several Methods for Drawing Images, Shapes and Text.  
    */
    public class Renderer
    {
        public uint pixelWidth { get; private set; } //!< Width of the screen in pixels
        public uint pixelHeight { get; private set; } //!< Height of the screen in pixels

        private GCHandle pixelBufferHandle;

        private uint drawColorBit = 0;
        private Camera curCamera = new Camera(0,0);

        public Renderer(uint _pixelWidth, uint _pixelHeight) {
            pixelWidth = _pixelWidth;
            pixelHeight = _pixelHeight;

            uint[] pixelbuffer = new uint[_pixelWidth * _pixelHeight];
            pixelBufferHandle = GCHandle.Alloc(pixelbuffer, GCHandleType.Pinned);
        }

        //! Draws a sprite onto the screen
        /*!
          \param _x X position of sprite
          \param _y Y position of sprite
          \param _sprite sprite to be rendered
          \sa Sprite, TextureSprite
        */
        public void DrawSprite(int _x, int _y, Sprite _sprite) {
            uint[]? pixelbuffer = pixelBufferHandle.Target as uint[];
            if (pixelbuffer == null) {
                Console.WriteLine("Error: Cannot access pixelbuffer!");
                return;
            }

            _x -= (int)curCamera.x;
            _y -= (int)curCamera.y;

            uint[] pxielData = _sprite.GetPixelData();

            for (int iy = 0; iy < _sprite.Height; iy++) {
                for (int ix = 0; ix < _sprite.Width; ix++) {
                    if (pxielData[iy * _sprite.Width + ix] == 0xffff00ff) continue;

                    if(_x + ix < 0 || _x + ix >= pixelWidth || _y + iy < 0 || _y + iy >= pixelHeight) continue;

                    pixelbuffer[(_y + iy) * (int)pixelWidth + (_x + ix)] = pxielData[iy * _sprite.Width + ix];
                }
            }  

        }
 
        //! Draws a rectangle onto the screen using the current draw color
        /*!
          \param _x X position of the rectangle
          \param _y Y position of the rectangle
          \param _w Width of the rectangle
          \param _h Height of the rectangle 
          \sa SetDrawColor
        */
        public void DrawRect(int _x, int _y, int _w, int _h) {
            uint[]? pixelbuffer = pixelBufferHandle.Target as uint[];
            if (pixelbuffer == null) {
                Console.WriteLine("Error: Cannot access pixelbuffer!");
                return;
            }

            _x -= (int)curCamera.x;
            _y -= (int)curCamera.y;

            for (int sy = _y; sy < _y+_h; sy++) {
                for (int sx = _x; sx < _x+_w; sx++) {
                    if (sx < 0 || sx >= pixelWidth || sy < 0 || sy >= pixelHeight) continue;

                    pixelbuffer[sy * (int)pixelWidth + sx] = drawColorBit;
                }
            }

        }

        //! Draws a text onto the screen.
        /*!
          \param _x X position of the text
          \param _y Y position of the text
          \param _text the actual text to be rendered. 
          \param _font the font to be used for rendering.
          \sa FPEFont
        */
        public void DrawText(int _x, int _y, string _text, FPEFont _font) {
            uint[]? pixelbuffer = pixelBufferHandle.Target as uint[];
            if (pixelbuffer == null) {
                Console.WriteLine("Error: Cannot access pixelbuffer!");
                return;
            }

            _x -= (int)curCamera.x;
            _y -= (int)curCamera.y;

            int _cursor = 0;
            int _yoff = 0;

            foreach (char _c in _text) {
                CharData _charData = _font.GetChar(_c);

                for (int iy = 0; iy < _charData.height; iy++) {
                    for (int ix = 0; ix < _charData.width; ix++) {
                        if (_charData.pixelData[iy * _charData.width + ix] == 0xffff00ff) continue;

                        int _px = (_x + _cursor + ix);
                        int _py = (_y + iy - _yoff);

                        if (_px < 0 || _px >= pixelWidth ||_py < 0 || _py >= pixelHeight) continue;

                        pixelbuffer[_py * (int)pixelWidth + _px] = _charData.pixelData[iy * _charData.width + ix];

                    }
                }

                if (_c == '\n') {
                    _cursor = 0;
                    _yoff -= (int)_charData.height + _font.VerticalSpacing;
                    continue;
                }

                _cursor += (int)_charData.width + _font.Kerning;
            }
        }

        //! Sets the draw color 
        /*!
          \param _drawColor The color (a vector 3 where (x,y,z) represents (r,g,b) in the color range of 0 - 255)
          \sa DrawRect
        */
        public void SetDrawColor(Vector3 _drawColor) {
            _drawColor = Vector3.Clamp(_drawColor, Vector3.Zero, new Vector3(255));
            drawColorBit = (uint)0xff000000 | (uint)_drawColor.X << 16 | (uint)_drawColor.Y << 8 | (uint)_drawColor.Z;
        }

        //! Sets the camera to be used for rendering.
        /*!
          \param _camera the camera 
          \sa Camera, PopCamera
        */
        public void PushCamera(Camera _camera) {
            curCamera = _camera;
        }

        //! Removes camera and makes future draw call relative to the screen. 
        /*!
          \sa Camera, PushCamera
        */
        public void PopCamera() {
            curCamera = new Camera(0,0);
        }

        //! Clears the screen with a set color.
        /*!
          \param _clearColor The color (a vector 3 where (x,y,z) represents (r,g,b) in the color range of 0 - 255)
        */
        public void ClearScreen(Vector3 _clearColor) {
            _clearColor = Vector3.Clamp(_clearColor, Vector3.Zero, new Vector3(255));
            uint _clearBit = (uint)0xff000000 | (uint)_clearColor.X << 16 | (uint)_clearColor.Y << 8 | (uint)_clearColor.Z;

            uint[]? pixelbuffer = pixelBufferHandle.Target as uint[];
            if (pixelbuffer == null) {
                Console.WriteLine("Error: Cannot access pixelbuffer!");
                return;
            }

            for (int i = 0; i < pixelbuffer.Length; i++) {
                pixelbuffer[i] = _clearBit;
            }
        }

        //! Gets the buffer of pixels
        /*!
        */
        public uint[]? GetPixelBuffer() {
            return pixelBufferHandle.Target as uint[];
        }

        public IntPtr GetPixelBufferPtr() { 
            return pixelBufferHandle.AddrOfPinnedObject();
        }

        
        public void Destroy() {
            pixelBufferHandle.Free();
        }
    }
}
