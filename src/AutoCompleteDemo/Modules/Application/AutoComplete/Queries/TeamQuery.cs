using AutoCompleteDemo.Models;
using AutoCompleteDemo.Modules.Application.AutoComplete.Options;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;
using Disqord;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Queries
{
    public class TeamQuery : QueryBase<TeamOptions, Team, TeamResult>
    {
        public TeamQuery(ICommandContextAccessor commandContextAccessor, LeagueQuery leagueQuery) : base(commandContextAccessor)
        {
            LeagueQuery = leagueQuery;
        }

        // The teams returned by the query could return on the league which may or may not have been selected
        public LeagueQuery LeagueQuery { get; }

        protected override Task<TeamOptions?> FetchOptionsAsync()
        {
            // This might be a database query
            Team[] options =
            {
                // nhl
                new Team(1, 1, "Washington Capitals"),
                new Team(2, 1, "Boston Bruins"),
                new Team(3, 1, "Seattle Kraken"),

                // nfl
                new Team(4, 2, "Baltimore Ravens"),
                new Team(5, 2, "Kansas City Chiefs"),
                new Team(6, 2, "Tampa Bay Buccaneers"),

                // ncaaf
                new Team(7, 3, "USF"),
                new Team(8, 3, "UCF"),
                new Team(9, 3, "FSU")
            };

            return LeagueQuery.Options?.Result.Result == null
                ? Task.FromResult(new TeamOptions(options))
                : Task.FromResult(new TeamOptions(options.Where(o => o.LeaugeId == LeagueQuery.Options.Result.Result.Id).ToArray()));
        }

        protected override void FillAutoComplete(TeamOptions options, AutoComplete<string> field)
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
