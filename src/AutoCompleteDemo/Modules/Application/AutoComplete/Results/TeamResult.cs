using AutoCompleteDemo.Models;
using AutoCompleteDemo.Modules.Application.AutoComplete.Options;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Results
{
    public class TeamResult : ResultBase<TeamOptions, Team>
    {
        public TeamResult(TeamOptions options, string rawValue, Team result) : base(options, rawValue, result)
        {
        }
    }
}
