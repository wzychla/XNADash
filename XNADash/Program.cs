using System;

namespace XNADash
{
    static class Program
    {
        public const string CurrentVersion = "0.57";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DashGame game = new DashGame())
            {
                game.Window.Title = $"XNADash {CurrentVersion} (c) 2011-{DateTime.Now.Year}, F1 for help";
                game.Run();
            }
        }
    }
}
