using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheGentleman2._0.Commands
{
    public class TestCommands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Alias("latency")]
        [Summary("Displays Bot's Latency")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync($"Pong! the bots ping is {Context.Client.Latency} ms");
        }

        [Command("8bal")]
        [Alias("eightball")]
        [Summary("Ask a question to the eightball")]
        public async Task eightball([Remainder] string question = "")
        {
            String[] answers = { "Yes", "No", "IDK", "Ask Again" };

            var embed = new EmbedBuilder()
            {
                Title = "Eightball",
                Author = new EmbedAuthorBuilder
                {
                    Name = Context.User.Username,
                },
                Color = new Color(0, 204, 255)
            };

            embed.AddField(x =>
            {
                x.Name = "Question";
                x.Value = question;
                x.IsInline = true;
            });

            embed.AddField(x =>
            {
                x.Name = "Answer";
                x.Value = answers[RandomInt(0 , answers.Length)];
                x.IsInline = true;
            });
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        private int RandomInt(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }
    }
}
