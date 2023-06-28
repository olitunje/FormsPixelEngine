using FormsPixelEngine.FPE.From;

namespace FlappyClone
{
    internal static class Program
    {
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new FlappyClone("FlappyClone", 1000, 600, 300, 200));
        }
    }
}