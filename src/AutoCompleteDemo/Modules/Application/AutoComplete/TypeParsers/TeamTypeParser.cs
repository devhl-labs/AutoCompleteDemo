using AutoCompleteDemo.Modules.Application.AutoComplete.Options;
using AutoCompleteDemo.Modules.Application.AutoComplete.Queries;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;
using Disqord.Bot.Commands;
using Qmmands;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.TypeParsers
{
    public class TeamTypeParser : DiscordTypeParser<TeamResult>
    {
        public override async ValueTask<ITypeParserResult<TeamResult>> ParseAsync(IDiscordCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            TeamQuery query = context.Services.GetRequiredService<TeamQuery>();

            TeamOptions? options = await query.GetOrFetchOptionsAsync();

            TeamResult? result = options?.GetResult(value.ToString());

            return result == null
                ? Failure($"Sorry, could not find team {value}")
                : Success(result);
        }
    }
}
