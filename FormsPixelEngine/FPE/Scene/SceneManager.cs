using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Scene
{
    /*! \class SceneManager
    \brief Allows the starting an changing scenes. 
    */
    public class SceneManager
    {
        public Scene? currentScene { get; private set; } /*!< Instance of the current running scene \sa Scene */

        private Renderer renderer;
        private InputSystem input;

        public SceneManager(Renderer _renderer, InputSystem _input) {
            renderer = _renderer;
            input = _input;
        }

        //! Changes the current scene. 
        /*!
          \param _scene The new scene
          \sa Scene
        */
        public void SetScene(Scene _scene) {
            currentScene?.Close();

            currentScene = _scene;
            currentScene.InitScene(renderer, input, this);        
        }
    }
}
