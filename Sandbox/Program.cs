namespace FormsPixelEngine
{
    internal static class Program
    {
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new Sandbox("Sandbox", 1500,1000, 300,200));
        }
    }
}