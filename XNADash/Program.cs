namespace XNADash
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DashGame game = new DashGame())
            {
                game.Run();
            }
        }
    }
}
