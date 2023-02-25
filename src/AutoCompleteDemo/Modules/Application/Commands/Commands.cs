using AutoCompleteDemo.Modules.Application.AutoComplete.Queries;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;
using AutoCompleteDemo.Modules.Application.AutoComplete.TypeParsers;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Qmmands;
using Qmmands.Text;
using Qommon;

namespace AutoCompleteDemo.Modules.Application.Commands
{
    public class Commands : DiscordApplicationModuleBase
    {
        public LeagueQuery LeagueQuery { get; }
        public TeamQuery TeamQuery { get; }

        public Commands(LeagueQuery leagueQuery, TeamQuery teamQuery)
        {
            LeagueQuery = leagueQuery;
            TeamQuery = teamQuery;
        }



        // A command to retrieve a league
        [SlashCommand("leagues")]
        public IDiscordCommandResult LeaguesCommand([Name("league")][CustomTypeParser(typeof(LeagueTypeParser))] LeagueResult leagueResult)
            => Response($"Found league {leagueResult.Result.Name}");

        [AutoComplete("leagues")]
        public async Task AutoCompleteLeagues(AutoComplete<string> league) => await LeagueQuery.AutoComplete(league);



        // A command to retrieve a team
        [SlashCommand("teams")]
        public IDiscordCommandResult TeamsCommand([Name("team")][CustomTypeParser(typeof(TeamTypeParser))] TeamResult teamResult)
            => Response($"Found team {teamResult.Result.Name}");

        [AutoComplete("teams")]
        public async Task AutoCompleteTeams(AutoComplete<string> team) => await TeamQuery.AutoComplete(team);




        // A command with two arguments where the options in the second depend on the choice in the first argument
        [SlashCommand("cascading_options")]
        public IDiscordCommandResult CascadingOptionsCommand(
            [Name("league")][CustomTypeParser(typeof(LeagueTypeParser))] LeagueResult leagueResult,
            [Name("team")][CustomTypeParser(typeof(TeamTypeParser))] TeamResult teamResult)
                => Response($"Found leauge {leagueResult.Result.Name} and team {teamResult.Result.Name}");

        [AutoComplete("cascading_options")]
        public async Task AutoCompleteTeamByLeage(AutoComplete<string> league, AutoComplete<string> team)
        {
            // populate the scoped services so we can cascade options in the second argument
            await LeagueQuery.GetOrFetchResultAsync(league.Argument.GetValueOrDefault());
            await TeamQuery.GetOrFetchResultAsync(team.Argument.GetValueOrDefault());

            await LeagueQuery.AutoComplete(league);
            await TeamQuery.AutoComplete(team);
        }
    }
}
