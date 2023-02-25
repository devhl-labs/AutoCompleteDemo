using AutoCompleteDemo.Modules.Application.AutoComplete.Results;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Options;

public abstract class OptionsBase<TOptions, TResult> where TResult : IAutoCompleteResult
{
    public OptionsBase(TOptions[] values)
    {
        Values = values;
    }

    public TOptions[] Values { get; }

    public TResult? Result { get; private set; }

    protected abstract TResult? SetResult(string value);

    public TResult? GetResult(string value)
    {
        Result = SetResult(value);

        return Result;
    }

    public abstract string AutoCompletionText(TOptions option);
}
