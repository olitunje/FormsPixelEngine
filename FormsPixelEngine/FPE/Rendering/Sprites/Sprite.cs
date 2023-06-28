using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Rendering.Sprites
{

    /*! \class Sprite
    \brief Represents a generic sprite

    This abstract class represents a generic sprite. It holds an array of Pixels, the Width and Height of a Sprite
    */
    public abstract class Sprite
    {
        public abstract uint Width { get; } //!< Width of the Sprite
        public abstract uint Height { get; }//!< Height of the Sprite

        //! Returns the image data. 
        /*!
          \return Array of unsigned Integers that represent the image pixel data. 
        */
        public abstract uint[] GetPixelData();
    }
}
