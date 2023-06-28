using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Rendering
{
    /*! \class Camera
    \brief Represents a camera.

    This class defines a camera and is used by the renderer.
    */
    public class Camera
    {
        public float x;
        public float y;

        //! Constructor of Camera
        /*!
          \param _x Camera x position.
          \param _x Camera y position.
          \sa Renderer.PushCamera
        */
        public Camera(float _x, float _y) {
            x = _x;
            y = _y;
        }

    }
}
