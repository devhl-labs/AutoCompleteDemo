using AutoCompleteDemo.Models;
using AutoCompleteDemo.Modules.Application.AutoComplete.Options;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Results
{
    public class LeagueResult : ResultBase<LeagueOptions, League>
    {
        public LeagueResult(LeagueOptions options, string rawValue, League result) : base(options, rawValue, result)
        {

        }
    }
}
