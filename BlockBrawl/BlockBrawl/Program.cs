using System;
using System.Windows.Forms;


namespace BlockBrawl
{
    public static class Program
    {
        [STAThread]

        static void Main()
        {
            PreConfigurations preConfig = new PreConfigurations();
            if (preConfig.ShowPreConfigWindow)
            {
                Application.Run(preConfig);
            }
            using (var game = new Game1(/*preConfig.fullScreen, preConfig.gameWidth, preConfig.gameHeight*/))
                game.Run();
        }
    }
}
