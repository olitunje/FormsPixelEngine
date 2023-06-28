using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Scene
{

    /*! \class Renderer
    \brief Scene that can be started at runtime. 

    This class like the GameWindow class gives access to the Renderer, Input and Scene system. A Scene can be started and changed at runtime. 
    */
    public abstract class Scene
    {
        protected Renderer renderer; /*!< Instance of the Render class for use in Rendering to the current Window \sa Renderer */
        protected InputSystem input;  /*!< Instance of the InputSystem class for use in getting Input for the current window \sa InputSystem */
        protected SceneManager sceneManager; /*!< Instance of the SceneManager class for use in managing scenes \sa SceneManager */
        protected float deltaTime;  //!< Times used to process last Frame. 

        public void InitScene(Renderer _renderer, InputSystem _input, SceneManager _sceneManager) {
            renderer = _renderer;
            input = _input;
            sceneManager = _sceneManager;

            Init();
        }

        public void UpdateScene(float _deltatime) { 
            deltaTime = _deltatime;

            Update();
        }

        //! Abstract Method that is called on Window Initialization
        /*!
          \sa OnUpdate(), OnClose()
        */
        public abstract void Init();

        //! Abstract Method that is called when the Window is Updated
        /*!
          \sa OnInit(), OnClose()
        */
        public abstract void Update();

        //! Abstract Method that is called when the Window is Closed
        /*!
          \sa OnInit(), OnUpdate()
        */
        public abstract void Close();


    }
}
