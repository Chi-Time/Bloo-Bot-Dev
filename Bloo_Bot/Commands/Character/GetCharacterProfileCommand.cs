using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Text;

namespace BlooBot.Commands.Character
{
    class GetCharacterProfileCommand
    {
        public async Task GetCharacterProfile (CommandContext ctx, DiscordMember member)
        {
            if (await ProfileExists (member.DisplayName))
            {
                await ctx.RespondAsync ("Sure thing");
                string profileJSON = "";

                using (var fs = File.OpenRead (await GetProfilePath (member.DisplayName)))
                using (var sr = new StreamReader (fs, new UTF8Encoding (false)))
                    profileJSON = await sr.ReadToEndAsync ();

                var character = Newtonsoft.Json.JsonConvert.DeserializeObject<Objects.CharacterSheet> (profileJSON);

                var embed = await GetEmbed (ctx, member, character);

                await ctx.RespondAsync ("", embed: embed);

                return;
            }

            await ctx.RespondAsync ($"Oh I can't do that {member.DisplayName}! Looks like they don't have a character profile yet. <3");
        }

        private async Task<DiscordEmbed> GetEmbed (CommandContext ctx, DiscordMember member, Objects.CharacterSheet character)
        {
            await Task.Yield ();

            var embed = new DiscordEmbedBuilder ()
            {
                Title = $"{member.DisplayName}'s Character Profile",
                ThumbnailUrl = member.AvatarUrl,
                Footer = new DiscordEmbedBuilder.EmbedFooter ()
                {
                    Text = "Hopefully they don't die too soon!"
                }
            };

            embed.AddField ("Character Name", character.Name, true);
            embed.AddField ("Health Remaining", character.Health.ToString (), true);
            embed.AddField ("Current Damage", character.Damage.ToString (), true);
            embed.AddField ("Current Weapon", character.Weapon.ToString (), true);
            embed.AddField ("Character Bio", character.Bio, false);

            return embed;
        }

        public async Task<string> GetProfilePath (string userName)
        {
            await Task.Yield ();

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = userName + ".json";

            return Path.Combine (appPath, fileName);
        }

        public async Task<bool> ProfileExists (string userName)
        {
            await Task.Yield ();

            string filePath = await GetProfilePath (userName);

            if (File.Exists (filePath))
                return true;

            return false;
        }
    }
}
