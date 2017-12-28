using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace BlooBot.Commands.User
{
    class GetAvatarCommand
    {
        public async Task GetAvatar (CommandContext ctx, DiscordMember member)
        {
            await ctx.RespondAsync ("", embed: await GetEmbed (ctx, member));
        }

        private async Task<DiscordEmbed> GetEmbed (CommandContext ctx, DiscordMember member)
        {
            var footers = await GetFooters ();
            string title = $"{ctx.User.Username} requested { member.Username} 's avatar! ^^";

            var response = new DiscordEmbedBuilder ()
            {
                Title = title,
                Color = DiscordColor.Azure,
                ImageUrl = member.AvatarUrl,
                Footer = new DiscordEmbedBuilder.EmbedFooter ()
                {
                    Text = footers[Bloo._Rand.Next (0, footers.Length)]
                }
            };

            return response;
        }

        private async Task<string[]> GetFooters ()
        {
            await Task.Yield ();

            return new string[]
                 {
                    "Looks pretty good!",
                    "Aw yee. I like this one.",
                    "Well... I wonder they did that. -.^",
                    "Ooh! Coot."
                };
        }
    }
}
