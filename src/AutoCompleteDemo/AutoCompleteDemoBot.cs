using AutoCompleteDemo.Modules.Application.AutoComplete.TypeParsers;
using Disqord;
using Disqord.Bot;
using Microsoft.Extensions.Options;
using Qmmands.Default;

namespace AutoCompleteDemo
{
    public class AutoCompleteDemoBot : DiscordBot
    {
        public AutoCompleteDemoBot(IOptions<DiscordBotConfiguration> options, ILogger<DiscordBot> logger, IServiceProvider services, DiscordClient client) : base(options, logger, services, client)
        {
        }

        protected override ValueTask AddTypeParsers(DefaultTypeParserProvider typeParserProvider, CancellationToken cancellationToken)
        {
            typeParserProvider.AddParser(new LeagueTypeParser());
            typeParserProvider.AddParser(new TeamTypeParser());
            return base.AddTypeParsers(typeParserProvider, cancellationToken);
        }
    }
}
