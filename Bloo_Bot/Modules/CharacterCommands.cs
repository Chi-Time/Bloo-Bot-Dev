using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace BlooBot.Commands
{
    [Group ("character")]
    [Aliases ("char")]
    [Description ("Commands for interacting and reading character profiles.")]
    class CharacterCommands
    {
        [Command ("profile")]
        [Aliases ("p")]
        [Description ("Retrieves the current profile for the given user.")]
        public async Task GetProfile (CommandContext ctx, [Description ("The user to retrieve the profile of")] DiscordMember member)
        {
            await ctx.TriggerTypingAsync ();

            try
            {
                await new Character.GetCharacterProfileCommand ().GetCharacterProfile (ctx, member);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync ("Sorry! I can't seem to do that right now.\nError: " + e.Message);
            }
        }
    }
}
