using FormsPixelEngine.FPE.Input;
using FormsPixelEngine.FPE.Rendering.Sprites;
using FormsPixelEngine.FPE.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    internal class Choose : Scene
    {
        TextureSprite poke1;
        TextureSprite poke2;
        TextureSprite poke3;

        List<Pokemon> PokemonList;

        public override void Init()
        {
            PokemonList = new List<Pokemon>();
            PokemonList.Add(new Pokemon(30, 65, "./res/Textures/poke3.png"));
            PokemonList.Add(new Pokemon(48, 45, "./res/Textures/poke1.png"));
            PokemonList.Add(new Pokemon(58, 70, "./res/Textures/poke2.png"));

            input.SubscribeKey(KeyCode.Space);

        }

        public override void Update()
        {
            renderer.ClearScreen(new Vector3(0, 120, 0));

            if (input.KeyPressed(KeyCode.Space))
                sceneManager.SetScene(new SandboxScene());
            foreach (Pokemon p in PokemonList)
            {

                renderer.DrawSprite(p.x, p.y, p.GetTextureSprite());

            }
        }

        public override void Close()
        {
            input.UnsubscribeKey(KeyCode.Space);
        }
    }
}
