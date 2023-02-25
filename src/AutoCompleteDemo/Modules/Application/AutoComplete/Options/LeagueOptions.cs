using AutoCompleteDemo.Models;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Options
{
    public class LeagueOptions : OptionsBase<League, LeagueResult>
    {
        public LeagueOptions(League[] options) : base(options)
        {
        }

        public override string AutoCompletionText(League option) => option.Name;

        protected override LeagueResult? SetResult(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            League? league = Values.FirstOrDefault(t => AutoCompletionText(t).Equals(value)) ?? Values.FirstOrDefault(v => v.Name.ToLower().Contains(value.ToLower()));

            return league == null
                ? null
                : new LeagueResult(this, value, league);
        }
    }
}
