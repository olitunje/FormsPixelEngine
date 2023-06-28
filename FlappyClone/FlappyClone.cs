using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsPixelEngine.FPE.From;

namespace FlappyClone
{
    public class FlappyClone : GameWindow
    {
        public FlappyClone(string _title, uint _windowWidth, uint _windowHeight, uint _pixelWidth, uint _pixelHeight) : base(_title, _windowWidth, _windowHeight, _pixelWidth, _pixelHeight) {
        }

        public override void OnInit() {
            sceneManager.SetScene(new MainMenu());
        }

        public override void OnUpdate() {
        }
        public override void OnClose() {
        }
    }
}
