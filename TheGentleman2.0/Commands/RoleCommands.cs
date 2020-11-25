using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGentleman2._0.Commands
{
    public class RoleCommands : ModuleBase<SocketCommandContext>
    {
        [Command("getrole")]
        public async Task GetRole([Remainder] string rolename)
        {
            var role = Context.Guild.GetRole(Context.Message.MentionedRoles.First().Id);
            var embed = new EmbedBuilder
            {
                Title = $"Info on role {role.Name}",
                Color = role.Color
            };
            embed.AddField("Admin", role.Permissions.Administrator ? "Yes" : "No");
            await Context.Channel.SendMessageAsync(embed: embed.Build());

        }
    }
}
