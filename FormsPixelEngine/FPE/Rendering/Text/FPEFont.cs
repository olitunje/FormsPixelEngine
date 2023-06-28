using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Rendering.Text
{
    /*! \class FPEFont
    \brief Represents a font and is used for text rendering

    This class loads a font file and is used to render text.
    */
    public class FPEFont
    {
        public int VerticalSpacing = 10; //!< Defines the space between lines of Text. 
        public int Kerning = 0; //!< Defines the space between text characters. 

        private Dictionary<char, CharData> charset;

        //! Constructor of FPEFont
        /*!
          \param _fontpath The filepath of the .fpeFont file. 
          \param _texturepath The filepath of the font texture file. 
        */
        public FPEFont(string _fontpath, string _texturepath) {
            charset = new Dictionary<char, CharData>();

            Bitmap _textureImg = new Bitmap(Image.FromFile(_texturepath));

            string _filedata = File.ReadAllText(_fontpath);
            string[] _lines = _filedata.Split("\n");

            for (int i = 1; i < _lines.Length; i++) {
                string _line = _lines[i].Trim();
                if (string.IsNullOrEmpty(_line)) continue;

                string c = _line.Split("=")[0].Trim();
                string[] _data = _line.Split("=")[1].Split(",");

                if (c == "EQL") c = "=";
                if (c == "SP") c = " ";

                CharData _charData = new CharData();
                _charData.org = c[0];
                _charData.width = uint.Parse(_data[2].Trim());
                _charData.height = uint.Parse(_data[3].Trim());
                _charData.pixelData = new uint[_charData.width * _charData.height];

                uint _offx = uint.Parse(_data[0].Trim());
                uint _offy = uint.Parse(_data[1].Trim());

                for (int y = 0; y < _charData.height; y++) {
                    for (int x = 0; x < _charData.width; x++) {
                        _charData.pixelData[y * _charData.width + x] = (uint)_textureImg.GetPixel((int)(x + _offx), (int)(y + _offy)).ToArgb();
                    }
                }

                charset.Add(_charData.org, _charData);
            }

            Console.WriteLine($"Loaded Font '{_fontpath}'");
        }

        //! Returns the information of a character as a CharData struct. 
        /*!
          \param _c The demanded character 
          \return Returns the character information as a CharData struct. 
          \sa CharData
        */
        public CharData GetChar(char _c) {
            if (!charset.ContainsKey(_c)) {
                CharData _nullData = new CharData();
                _nullData.org = ' ';
                _nullData.width = 0;
                _nullData.height = 0;
                _nullData.pixelData = new uint[0];
                return _nullData;
            }

            return charset[_c];
        }
    }

    /*! \class CharData
    \brief Represents the character information for text rendering

    This class holds the width, hight and pixel data for the rendering of Text
    */
    public struct CharData {
        public char org;
        public uint width, height;
        public uint[] pixelData;
    }
}
