using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace BlooBot
{
    class Bloo
    {
        public static Random _Rand { get; private set; }
        private DiscordClient _Client = null;
        private CommandsNextModule _Commands { get; set; }

        public Bloo ()
        {
            _Rand = new Random ();
        }

        public async Task StartBotAsync ()
        {
            string configJson = "";

            using (var fs = File.OpenRead ("config.json"))
            using (var sr = new StreamReader (fs, new UTF8Encoding (false)))
                configJson = await sr.ReadToEndAsync ();

            var botConfiguration = JsonConvert.DeserializeObject<Objects.BotConfig> (configJson);
            var config = new DiscordConfiguration
            {
                Token = botConfiguration.Token,
                TokenType = TokenType.Bot,

                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            this._Client = new DiscordClient (config);

            var commandConfig = new CommandsNextConfiguration
            {
                StringPrefix = botConfiguration.CommandPrefix,
                EnableDms = true,
                EnableMentionPrefix = true
            };

            this._Commands = this._Client.UseCommandsNext (commandConfig);

            this._Commands.CommandExecuted += this.CommandsExecuted;
            this._Commands.CommandErrored += this.CommandsErrored;

            this._Commands.RegisterCommands<Commands.UserCommands> ();

            this._Client.Ready += this.ClientReady;

            await this._Client.ConnectAsync ();

            await Task.Delay (-1);
        }

        private Task ClientReady (ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage (LogLevel.Info, "Bloo Bot", "Client is ready to process events", DateTime.Now);

            return Task.CompletedTask;
        }

        private Task CommandsExecuted (CommandExecutionEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage (LogLevel.Info, "Bloo bot", $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}'", DateTime.Now);

            return Task.CompletedTask;
        }

        private async Task CommandsErrored (CommandErrorEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage (LogLevel.Error, "ExampleBot", $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType ()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            // let's check if the error is a result of lack
            // of required permissions
            if (e.Exception is ChecksFailedException ex)
            {
                var emoji = DiscordEmoji.FromName (e.Context.Client, ":no_entry:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access denied",
                    Description = $"{emoji} You do not have the permissions required to execute this command.",
                    Color = DiscordColor.Red
                };

                await e.Context.RespondAsync ("", embed: embed);
            }
        }
    }
}
