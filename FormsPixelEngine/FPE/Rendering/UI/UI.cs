using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering.Sprites;
using FormsPixelEngine.FPE.Rendering.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Rendering.UI
{
    public class UI
    {
        private List<UI_Element> elements;
        private Dictionary<Element_Type, Action<UI_Element>> ElementHandlers;

        private Renderer renderer;
        private InputSystem inputSystem;

        private uint drawColorBit = 1;
        
        public FPEFont mainFont;
        public TextureSprite buttonSprite;
        public TextureSprite buttonDownSprite;

        public UI(Renderer _renderer, InputSystem _inputSystem) {
            renderer = _renderer;
            inputSystem = _inputSystem;

            ElementHandlers = new Dictionary<Element_Type, Action<UI_Element>> {
                { Element_Type.BUTTON,  UpdateButton },
                { Element_Type.LABLE,   UpdateLable },
                { Element_Type.TEXTBOX, UpdateTextbox }
            };
        }

        public void UpdateUI() {
            foreach (UI_Element _e in elements) {
                ElementHandlers[_e.Type](_e);
            }
        }

        private void UpdateButton(UI_Element _element) {

        }

        private void UpdateLable(UI_Element _element) {
              
        }

        private void UpdateTextbox(UI_Element _element) {
               
        }

        public void LoadStyleAssets(string _filepath) {
            mainFont = new FPEFont(_filepath + "uifont.fpeFont", _filepath + "uifonttexture.fpeFont");
            buttonSprite = new TextureSprite(_filepath + "buttonsprite.png");
            buttonDownSprite = new TextureSprite(_filepath + "buttondownsprite.png");
        }

        public void AddButton(string _name, int _x, int _y, int _w, int _h, Action<UI_Element> _onPress) { 
            UI_Element _button = new UI_Element();
            _button.Name = _name;
            _button.Position = new Vector2(_x,_y);
            _button.Size = new Vector2(_w,_h);
            _button.OnAction += _onPress;

            elements.Add(_button);
        }

        public void RemoveButton(string _name) {
            foreach (UI_Element _e in elements) {
                if (_e.Name == _name) { 
                    elements.Remove(_e);
                    continue;
                } 
            }

        }

        public void SetDrawColor(Vector3 _drawColor) {
            _drawColor = Vector3.Clamp(_drawColor, Vector3.Zero, new Vector3(255));
            drawColorBit = (uint)0xff000000 | (uint)_drawColor.X << 16 | (uint)_drawColor.Y << 8 | (uint)_drawColor.Z;
        }

    }
}
