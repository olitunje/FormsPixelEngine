using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsPixelEngine.FPE.From
{
    public abstract partial class GameWindow : Form
    {
        public GameWindow(string _title, uint _windowWidth, uint _windowHeight, uint _pixelWidth, uint _pixelHeight) {
            InitializeComponent(_title, _windowWidth, _windowHeight, _pixelWidth, _pixelHeight);
        }
    }
}
