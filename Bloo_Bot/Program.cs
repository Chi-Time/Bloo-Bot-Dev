namespace BlooBot
{
    class Program
    {
        static void Main (string[] args) => new Program ().EntryPoint ();

        private void EntryPoint ()
        {
            var bot = new Bloo ();
            bot.StartBotAsync ().GetAwaiter ().GetResult ();
        }
    }
}
