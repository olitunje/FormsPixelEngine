using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering;
using FormsPixelEngine.FPE.Rendering.Sprites;
using FormsPixelEngine.FPE.Rendering.Text;
using FormsPixelEngine.FPE.Scene;
using System.Numerics;

namespace Sandbox
{
    public class SandboxScene : Scene
    {
        Camera camera;
        FPEFont fontlarge;
        FPEFont fontsmall;
        TextureSprite Pokeball;
        TextureSprite bg;
        TextureSprite ui;

        List<Pokemon> PokemonList;

        TextureSprite[] spriteSheetUp;
        TextureSprite[] spriteSheetDown;
        TextureSprite[] spriteSheetLeft;
        TextureSprite[] spriteSheetRight;
        float animationTimer;
        float xpos = 32;
        float ypos = 132;

        public override void Init() {
            input.SubscribeKey(KeyCode.W);
            input.SubscribeKey(KeyCode.A);
            input.SubscribeKey(KeyCode.S);
            input.SubscribeKey(KeyCode.D);

            input.SubscribeKey(KeyCode.Left);
            input.SubscribeKey(KeyCode.Right);
            input.SubscribeKey(KeyCode.Up);
            input.SubscribeKey(KeyCode.Down);

            camera = new Camera(0, 0);

            fontlarge = new FPEFont("./res/Font/boxy_bold_font.fpeFont", "./res/Font/boxy_bold_font.png");
            fontsmall = new FPEFont("./res/Font/simple.fpeFont", "./res/Font/simple.png");

            PokemonList = new List<Pokemon>();
            



            Pokeball = new TextureSprite("./res/Textures/Pokeball.png");
            bg = new TextureSprite("./res/Textures/peakpx.png");
            ui = new TextureSprite("./res/Textures/ui.png");

            spriteSheetUp = new TextureSprite[4];
            spriteSheetDown = new TextureSprite[4];
            spriteSheetLeft = new TextureSprite[4];
            spriteSheetRight = new TextureSprite[4];

            for (uint j = 0; j < 4; j++) {
                spriteSheetUp[j] = new TextureSprite("./res/Textures/SheetDemo.png", 32, j, 3);         
            }

            for (uint j = 0; j < 4; j++)
            {
                spriteSheetDown[j] = new TextureSprite("./res/Textures/SheetDemo.png", 32, j, 0);
            }

            for (uint j = 0; j < 4; j++)
            {
                spriteSheetLeft[j] = new TextureSprite("./res/Textures/SheetDemo.png", 32, j, 1);
            }

            for (uint j = 0; j < 4; j++)
            {
                spriteSheetRight[j] = new TextureSprite("./res/Textures/SheetDemo.png", 32,j, 2);
            }
        }

        public override void Update() {
            // Update -------------
            if (input.KeyPressed(KeyCode.S)) ypos += 25 * deltaTime;
            if (input.KeyPressed(KeyCode.W)) ypos -= 25 * deltaTime;
            if (input.KeyPressed(KeyCode.D)) xpos += 25 * deltaTime;
            if (input.KeyPressed(KeyCode.A)) xpos -= 25 * deltaTime;

            if (input.KeyPressed(KeyCode.Down)) camera.y += 30 * deltaTime;
            if (input.KeyPressed(KeyCode.Up)) camera.y -= 30 * deltaTime;
            if (input.KeyPressed(KeyCode.Right)) camera.x += 30 * deltaTime;
            if (input.KeyPressed(KeyCode.Left)) camera.x -= 30 * deltaTime;

            animationTimer += deltaTime;
            if (animationTimer > 1) animationTimer = 0;
            uint index = (uint)(animationTimer * 4);

            // Render -------------
            renderer.PushCamera(camera);
            renderer.ClearScreen(new Vector3(0, 120, 0));

            renderer.DrawSprite(0, 0, bg);
            
            if (input.KeyPressed(KeyCode.S)) renderer.DrawSprite((int)xpos, (int)ypos, spriteSheetDown[index]);
            else if (input.KeyPressed(KeyCode.W)) renderer.DrawSprite((int)xpos, (int)ypos, spriteSheetUp[index]);
            else if (input.KeyPressed(KeyCode.D)) renderer.DrawSprite((int)xpos, (int)ypos, spriteSheetRight[index]);
            else if (input.KeyPressed(KeyCode.A)) renderer.DrawSprite((int)xpos, (int)ypos, spriteSheetLeft[index]);
            else renderer.DrawSprite((int)xpos, (int)ypos, spriteSheetDown[0]);

            

            renderer.PopCamera(); // not dependable from cam

            // UI ------------------------------

            renderer.DrawSprite(213, 5, ui);
            renderer.DrawSprite(279, 10, Pokeball);
            renderer.DrawText(220, 25, "Pokemon caught", fontsmall);
        }

        public override void Close() {
        }

    }
}
