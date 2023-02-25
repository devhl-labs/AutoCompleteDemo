namespace AutoCompleteDemo.Modules.Application.AutoComplete.Results;

public class ResultBase<TOptions, TResult> : IAutoCompleteResult
{
    public TOptions Options { get; }
    public string RawValue { get; }
    public TResult Result { get; }

    public ResultBase(TOptions options, string rawValue, TResult result)
    {
        Options = options;
        RawValue = rawValue;
        Result = result;
    }
}
