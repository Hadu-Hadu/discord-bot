using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheGentleman2._0.Commands
{
    public class AvatarCommand : ModuleBase<SocketCommandContext>
    {
        [Command("avatar")]
        [Alias("picture")]
        [Summary("Shows avatar of the person mentioned")]
        public async Task Avatar(IGuildUser user) 
        {
            string avatarURL = user.GetAvatarUrl(format: ImageFormat.Auto, 1024);

            if (avatarURL is null)
            {
                await Context.Message.Channel.SendMessageAsync($"{user.Mention} has no profile picture");
                return;
            }
            var embed = new EmbedBuilder();
            embed.WithColor(new Color(0, 204, 255));
            embed.WithTitle($"{user.Username}'s avatar");
            embed.WithUrl(avatarURL);
            embed.WithImageUrl(avatarURL);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
