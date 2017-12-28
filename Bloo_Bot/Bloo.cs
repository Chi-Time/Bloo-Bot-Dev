using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using BlooBot.Objects;

namespace BlooBot
{
    class Bloo
    {
        private DiscordClient _Client = null;

        public async Task StartBotAsync ()
        {
            string configJson = "";

            using (var fs = File.OpenRead ("config.json"))
            using (var sr = new StreamReader (fs, new UTF8Encoding (false)))
                configJson = await sr.ReadToEndAsync ();

            var botConfiguration = JsonConvert.DeserializeObject<BotConfig> (configJson);
            var config = new DiscordConfiguration
            {
                Token = botConfiguration.Token,
                TokenType = TokenType.Bot,

                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            this._Client = new DiscordClient (config);

            this._Client.Ready += this.ClientReady;

            await this._Client.ConnectAsync ();

            await Task.Delay (-1);
        }

        private Task ClientReady (ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage (LogLevel.Info, "Bloo Bot", "Client is ready to process events", DateTime.Now);

            return Task.CompletedTask;
        }
    }
}
