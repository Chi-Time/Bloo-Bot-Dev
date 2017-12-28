using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace BlooBot.Commands.User
{
    class GetUserInfoCommand
    {
        public async Task GetUser (CommandContext ctx, DiscordMember member)
        {
            var response = await GetEmbed (member);

            await ctx.RespondAsync ("", embed: response);
        }

        /// <summary>Creates and returns a formatted embed of the user's information.</summary>
        /// <param name="member">The member to get and format the information of.</param>
        private async Task<DiscordEmbed> GetEmbed (DiscordMember member)
        {
            var roles = await GetFormattedRoles (member);

            var response = new DiscordEmbedBuilder ()
            {
                Title = $"{member.Username}'s Information",
                ThumbnailUrl = member.AvatarUrl,
            };

            response.AddField ("Nickname", member.Nickname == null ? "None" : member.Nickname, true);
            response.AddField ("User ID", member.Id.ToString (), true);
            response.AddField ("Verified", member.Verified == false ? "No" : "Yes", true);
            response.AddField ("User Status", member.Presence == null ? "Offline" : member.Presence.Status.ToString (), true);
            //response.AddField ("Account Created", member.Verified == false ? "No" : "Yes", true);
            //response.AddField ("Joined Server", member.Verified == false ? "No" : "Yes", true);
            response.AddField ($"Roles [{ member.Roles.Count ()}]", roles, false);

            return response;
        }

        /// <summary>Creates and retrieves a formatted string of the users current roles in this server.</summary>
        /// <param name="member">The member to get the roles of.</param>
        private async Task<string> GetFormattedRoles (DiscordMember member)
        {
            await Task.Yield ();

            var currentRoles = string.Empty;
            var roles = member.Roles.ToArray ();

            // Loop through every role and add it with a comma, if it's the last role, don't add a comma.
            for (int i = 0; i < roles.Length; i++)
            {
                if (i == roles.Length - 1)
                    currentRoles += roles[i].Name;
                else
                    currentRoles += roles[i].Name + ", ";
            }

            return currentRoles;
        }
    }
}
