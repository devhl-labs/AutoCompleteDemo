using AutoCompleteDemo.Models;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;
namespace AutoCompleteDemo.Modules.Application.AutoComplete.Options
{
    public class TeamOptions : OptionsBase<Team, TeamResult>
    {
        public TeamOptions(Team[] teams) : base(teams)
        {
        }

        public override string AutoCompletionText(Team option) => option.Name;

        protected override TeamResult? SetResult(string value)
        {
            Team? team = Values.FirstOrDefault(t => AutoCompletionText(t).Equals(value)) ?? Values.FirstOrDefault(t => t.Name.ToLower().Contains(value.ToLower()));

            return team == null
                ? null
                : new TeamResult(this, value, team);
        }
    }
}
