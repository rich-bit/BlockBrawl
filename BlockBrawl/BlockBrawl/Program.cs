using System;
using System.Windows.Forms;


namespace BlockBrawl
{
    public static class Program
    {
        public static Game1 Game;

        [STAThread]

        static void Main()
        {
            PreConfigurations preConfig = new PreConfigurations();
            if (preConfig.ShowPreConfigWindow)
            {
                Application.Run(preConfig);
            }

            using (var game = new Game1())
            {
                Game = game;
                Game.Run();
            }
        }
    }
}
