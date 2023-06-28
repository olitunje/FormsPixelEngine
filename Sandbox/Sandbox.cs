using FormsPixelEngine.FPE.From;
using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering;
using FormsPixelEngine.FPE.Rendering.Sprites;
using FormsPixelEngine.FPE.Rendering.Text;
using Sandbox;
using System;
using System.Numerics;


namespace FormsPixelEngine
{
    internal class Sandbox : GameWindow
    {
        FPEFont fontlarge;

        // FPS
        uint fpsCounter = 0;
        uint fpsTrackCount = 25;
        float fpsAdder = 0;
        float fps;
        // ---

        public Sandbox(string _title, uint _windowWidth, uint _windowHeight, uint _pixelWidth, uint _pixelHeight) : base(_title, _windowWidth, _windowHeight, _pixelWidth, _pixelHeight) {
        }

        public override void OnInit() {
            fontlarge = new FPEFont("./res/Font/boxy_bold_font.fpeFont", "./res/Font/boxy_bold_font.png");
            GameState.InitState();
            sceneManager.SetScene(new MainMenuScene());
        }

        public override void OnUpdate() {
            // FPS ------
            fpsCounter++;
            fpsAdder += 1 / deltaTime;

            if (fpsCounter >= fpsTrackCount) {
                fps = fpsAdder / fpsTrackCount;
                fpsCounter = 0;
                fpsAdder = 0;
            }

            renderer.DrawText(5,5, $"FPS {(int)fps}", fontlarge);
        }

        public override void OnClose() {

        }
    }
}
