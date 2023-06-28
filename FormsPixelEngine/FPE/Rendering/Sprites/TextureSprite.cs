using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Rendering.Sprites
{
    /*! \class TextureSprite
    \brief Represents a sprite loaded from a texture file

    This class is a simple sprite loaded from a texture file. It holds an array of Pixels, the Width and Height of a Sprite
    */
    public class TextureSprite : Sprite
    {
        private uint[] pixelData;

        public override uint Width { get; } //!< Width of the Sprite
        public override uint Height { get; } //!< Height of the Sprite


        //! Constructor of TextureSprite
        /*!
          \param _filePath The filepath of the texture file. 
        */
        public TextureSprite(string _filePath) {
            Bitmap _textureImg = new Bitmap(Image.FromFile(_filePath));

            pixelData = new uint[_textureImg.Width * _textureImg.Height];
            Width = (uint)_textureImg.Width;
            Height = (uint)_textureImg.Height;

            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    pixelData[y * Width + x] = (uint)_textureImg.GetPixel(x, y).ToArgb();
                }
            }

            Console.WriteLine($"Loaded Texture [{Width}x{Height}]: '{_filePath}'");
        }

        //! Constructor of TextureSprite that loads a spriteheet
        /*!
            Loads a small segment of an image file (mainly spriteheet). 
          \param _filePath The filepath of the spriteheet file. 
          \param _segmentSize The size of a segments of the spriteheet.
          \param _xIndex Defines the colum of the segment. 
          \param _yIndex Defines the row of the segment. 
        */
        public TextureSprite(string _filePath, uint _segmentSize, uint _xIndex, uint _yIndex) {
            Bitmap _textureImg = new Bitmap(Image.FromFile(_filePath));
            
            if (_segmentSize * _xIndex > (uint)_textureImg.Width - _segmentSize || _segmentSize * _yIndex > (uint)_textureImg.Height - _segmentSize) {
                Console.WriteLine("Cannot Load Spritesheet: Spritesheet segment is outside of Texture bounds!");
                pixelData = new uint[0];
                return;
            }

            Width = _segmentSize; Height = _segmentSize;

            pixelData = new uint[_segmentSize * _segmentSize];
            uint _segmentX = _xIndex * _segmentSize;
            uint _segmentY = _yIndex * _segmentSize;

            for (int y = 0; y < _segmentSize; y++) {
                for (int x = 0; x < _segmentSize; x++) {
                    pixelData[y * _segmentSize + x] = (uint)_textureImg.GetPixel((int)_segmentX + x, (int)_segmentY + y).ToArgb();
                }
            }

            Console.WriteLine($"Loaded Texture [{Width}x{Height}]: '{_filePath}'");
        }

        public override uint[] GetPixelData() {
            return pixelData;
        }
    }
}
