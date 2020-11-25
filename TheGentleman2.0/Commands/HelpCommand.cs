using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TheGentleman2._0.Commands
{
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _Service;

        public HelpCommand(CommandService Service)
        {
            _Service = Service;
        }

        [Command("help")]
        [Alias("command", "commands")]
        [Summary("Displays all commands available")]

        public async Task Help([Remainder]String command = "")
        {
            string prefix = BotConfig.Config.BotPrefix;

            if (command == "")
            {
                var embed = new EmbedBuilder()
                {
                    Color = new Color(0, 204, 255),
                    Description = "Availible commands",
                };

                foreach (var module in _Service.Modules)
                {
                    string description = "";
                    foreach (var cmd in module.Commands)
                    {
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                    }

                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        string name = module.Name;

                        embed.AddField(x =>
                        {
                            x.Name = name;
                            x.Value = description;
                            x.IsInline = false;
                        });
                    }
                }
                await ReplyAsync(embed: embed.Build());
            }
            else 
            {
                var result = _Service.Search(Context, command);

                if (!result.IsSuccess)
                {
                    await ReplyAsync($"No result for {command}");
                    return;
                }

                var embed = new EmbedBuilder()
                {
                    Color = new Color(0, 204, 255),
                    Description = $"Availible commands similar to {command}",
                };

                foreach (var match in result.Commands)
                {
                    var cmd = match.Command;
                    embed.AddField(x =>
                    {
                        x.Name = string.Join(", ", cmd.Aliases);
                        x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" + $"Summary {cmd.Summary}";
                        x.IsInline = false;
                    });
                }
                await ReplyAsync(embed: embed.Build());
            }
        }

    }
}
