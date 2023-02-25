using AutoCompleteDemo.Modules.Application.AutoComplete.Options;
using AutoCompleteDemo.Modules.Application.AutoComplete.Queries;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;
using Disqord.Bot.Commands;
using Qmmands;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.TypeParsers
{
    public class LeagueTypeParser : DiscordTypeParser<LeagueResult>
    {
        public override async ValueTask<ITypeParserResult<LeagueResult>> ParseAsync(IDiscordCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            LeagueQuery query = context.Services.GetRequiredService<LeagueQuery>();

            LeagueOptions? options = await query.GetOrFetchOptionsAsync();

            LeagueResult? result = options?.GetResult(value.ToString());

            return result == null
                ? Failure($"Sorry, could not find league { value }")
                : Success(result);
        }
    }
}
