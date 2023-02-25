using AutoCompleteDemo.Models;
using AutoCompleteDemo.Modules.Application.AutoComplete.Options;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;
using Disqord;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Queries
{
    public class LeagueQuery : QueryBase<LeagueOptions, League, LeagueResult>
    {
        public LeagueQuery(ICommandContextAccessor accessor) : base(accessor)
        {
        }

        protected override Task<LeagueOptions?> FetchOptionsAsync()
        {
            // This might be a database query
            League[] options =
            {
                new League(1, "NHL"),
                new League(2, "NFL"),
                new League(3, "NCAAF")
            };

            return Task.FromResult(new LeagueOptions(options));
        }

        protected override void FillAutoComplete(LeagueOptions options, AutoComplete<string> field)
        {
            if (!field.IsFocused)
                return;

            if (string.IsNullOrWhiteSpace(field.RawArgument))
                field.Choices.AddRange(options.Values
                    .Select(options.AutoCompletionText)
                    .OrderBy(t => t)
                    .Distinct()
                    .Take(Discord.Limits.ApplicationCommand.Option.MaxChoiceAmount));
            else
                field.Choices.AddRange(options.Values
                    .Where(m => m.Name.ToLower().Contains(field.RawArgument.ToLower()))
                    .Select(options.AutoCompletionText)
                    .OrderBy(t => t)
                    .Distinct()
                    .Take(Discord.Limits.ApplicationCommand.Option.MaxChoiceAmount));
        }
    }
}
