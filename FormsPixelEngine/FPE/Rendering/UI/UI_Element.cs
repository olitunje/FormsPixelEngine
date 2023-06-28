using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FormsPixelEngine.FPE.Rendering.UI
{
    public struct UI_Element {
        public Element_Type Type;

        public string Name;
        public string Value;
        public Vector2 Position;
        public Vector2 Size;
        public Action<UI_Element> OnAction;
    }

    public enum Element_Type {
        LABLE,
        BUTTON,
        TEXTBOX,
    }
}
