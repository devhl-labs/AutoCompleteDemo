using AutoCompleteDemo.Modules.Application.AutoComplete.Queries;
using Disqord.Bot.Hosting;
using Disqord.Gateway;

namespace AutoCompleteDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScoped<LeagueQuery>();
                    services.AddScoped<TeamQuery>();
                })
                .ConfigureDiscordBot<AutoCompleteDemoBot>((hostBuilder, bot) =>
                {
                    bot.Token = hostBuilder.Configuration["AutoCompleteDemo"];
                    bot.Intents = GatewayIntents.DirectMessages | GatewayIntents.GuildMessages;
                })
                .Build();

            host.Run();
        }
    }
}