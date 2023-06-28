using FormsPixelEngine.FPE.Rendering.Text;
using FormsPixelEngine.FPE.Scene;
using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Sound;
using System;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsPixelEngine.FPE.Rendering.Sprites;
using System.Security.Policy;
using Sandbox;

namespace FormsPixelEngine
{
    internal class MainMenuScene : Scene
    {
        private FPEFont menuFont;
        private TextureSprite Logo;
        public override void Init() {
            input.SubscribeKey(KeyCode.Space);
            menuFont = new FPEFont("./res/Font/boxy_bold_font.fpeFont", "./res/Font/boxy_bold_font.png");
            Logo = new TextureSprite("./res/Textures/Logo2.png");
        }

        public override void Update() {
            renderer.ClearScreen(new Vector3(200, 255, 220));
            renderer.DrawText(50, 100, "Press Space to start the Game", menuFont);

            if (input.KeyPressed(KeyCode.Space))
                sceneManager.SetScene(new Choose());

            
            renderer.DrawSprite(50, 20, Logo);
        }

        public override void Close() {
            input.UnsubscribeKey(KeyCode.Space);
        }
    }
}
