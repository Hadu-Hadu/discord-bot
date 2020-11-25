using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheGentleman2._0
{
    public class Bot
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private IServiceProvider Service; //Integrate and Dependancy

        public Bot()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            { 
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel =LogSeverity.Debug //Debug sends everyting Verbose is only for nessesary
            });
        }

        public async Task MainAsync()
        {
            CommandHandler cmdHandler = new CommandHandler(Client, Commands);
            await cmdHandler.InitializeAsync();

            Client.Ready += Ready_Event; //subscribe(recreating while using) to event(ready event executes when its ready)
            Client.Log += Client_Log;
            if (BotConfig.Config.BotToken == "" || BotConfig.Config.BotToken == null);

            await Client.LoginAsync(TokenType.Bot, BotConfig.Config.BotToken);
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage msg)
        {
            Console.WriteLine($"{DateTime.Now} At {msg.Source}: {msg.Message}");
            return Task.CompletedTask;
        }

        private async Task Ready_Event()
        {
            Console.WriteLine($"{Client.CurrentUser.Username} is ready");
            await Client.SetGameAsync($"{BotConfig.Config.BotPrefix}help");
            await Client.SetStatusAsync(UserStatus.Online);
        }
    }
}
