using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheGentleman2._0
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _Client;
        private readonly CommandService _Commands;

        public CommandHandler(DiscordSocketClient Client, CommandService Commands)
        {
            _Client = Client;
            _Commands = Commands;
        }

        public async Task InitializeAsync()
        {
            await _Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            _Commands.Log += Commands_Log;
            _Client.MessageReceived += Message_Event;
        }

        private async Task Message_Event(SocketMessage MessageParameter)
        {
            var msg = MessageParameter as SocketUserMessage;
            var Context = new SocketCommandContext(_Client, msg);

            if (Context.Message == null || Context.Message.Content == "")return;
            if (Context.User.IsBot)return;

            int ArgPos = 0;
            if (!(msg.HasStringPrefix(BotConfig.Config.BotPrefix, ref ArgPos) || msg.HasMentionPrefix(_Client.CurrentUser, ref ArgPos)))return;

            var Result = await _Commands.ExecuteAsync(Context, ArgPos, null);
            if (!Result.IsSuccess && Result.Error != CommandError.UnknownCommand)
            {
                Console.WriteLine($"{DateTime.Now} at Command: {_Commands.Search(Context, ArgPos).Commands[0].Command.Name} in {_Commands.Search(Context, ArgPos).Commands[0].Command.Module.Name}: {Result.ErrorReason}");
                var embed = new EmbedBuilder();

                if (Result.ErrorReason == "The Command Is Incomplete")
                {
                    embed.WithTitle("##ERROR##");
                    embed.WithDescription("This command is incomplete check help page");
                }
                else
                {
                    embed.WithTitle("##ERROR##");
                    embed.WithDescription(Result.ErrorReason);
                }
                await Context.Channel.SendMessageAsync("", embed: embed.Build()); //.Build is required throws an error
            }

        }

        private Task Commands_Log(LogMessage Command)
        {
            Console.WriteLine(Command.Message);
            return Task.CompletedTask;
        }
    }
}
