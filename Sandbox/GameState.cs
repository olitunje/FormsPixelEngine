using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class GameState
    {
        public static GameState current;
        public static void InitState() {
            current = new GameState();
        }

        public List<Pokemon> myPokemon;
    }
}
