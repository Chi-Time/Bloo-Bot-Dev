using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace BlooBot.Commands
{
    [Group ("user")]
    [Description ("Commands for user profiles")]
    class UserCommands
    {
        [Command ("avatar")]
        [Description ("Grabs a mentioned users avatar.")]
        [Aliases ("a")]
        public async Task GetAvatar (CommandContext ctx, [Description ("The user you want the avatar of.")] DiscordMember member)
        {
            await ctx.TriggerTypingAsync ();

            try
            {
                await new User.GetAvatarCommand ().GetAvatar (ctx, member);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync ("Sorry! Seems like I can't do that right now.\nError: " + e.Message);
            }
        }

        [Command ("user")]
        [Description ("Grabs a user and all of their information.")]
        [Aliases ("u")]
        public async Task GetUser (CommandContext ctx, [Description ("The discord member to grab")] DiscordMember member)
        {
            await ctx.TriggerTypingAsync ();

            try
            {
                await new User.GetUserInfoCommand ().GetUser (ctx, member);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync (e.Message);
            }
        }
    }
}
