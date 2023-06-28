using FormsPixelEngine.FPE.Rendering.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Pokemon
    {
        public int x, y;
        private TextureSprite Sprite;

        public Pokemon(int x, int y, string filepath)
        {
            this.x = x;
            this.y = y;
            Sprite = new TextureSprite(filepath);
        }

        public TextureSprite GetTextureSprite() {
            return Sprite;
        }
    }
}
