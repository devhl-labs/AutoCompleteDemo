using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using AutoCompleteDemo.Modules.Application.AutoComplete.Options;
using AutoCompleteDemo.Modules.Application.AutoComplete.Results;

namespace AutoCompleteDemo.Modules.Application.AutoComplete.Queries;

public abstract class QueryBase<TOptionsBase, TOptions, TResult>
    where TOptionsBase : OptionsBase<TOptions, TResult>
    where TResult : IAutoCompleteResult
{
    public QueryBase(ICommandContextAccessor commandContextAccessor)
    {
        Context = commandContextAccessor.Context;
    }

    private volatile TOptionsBase? _options;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    public TOptionsBase? Options { get { return _options; } }
    public IDiscordCommandContext Context { get; }

    public async Task<TResult?> GetOrFetchResultAsync(string? value)
    {
        if (value == null)
            return default;

        TOptionsBase? options = await GetOrFetchOptionsAsync();
        if (options == null)
            return default;

        return options.GetResult(value);
    }

    public async Task<TOptionsBase?> GetOrFetchOptionsAsync()
    {
        if (_options != null)
            return _options;

        await _semaphore.WaitAsync();

        try
        {
            _options ??= await FetchOptionsAsync();
            return _options;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    protected abstract Task<TOptionsBase?> FetchOptionsAsync();

    protected abstract void FillAutoComplete(TOptionsBase options, AutoComplete<string> field);

    public async Task AutoComplete(AutoComplete<string> field)
    {
        if (!field.IsFocused)
            return;

        TOptionsBase? options = await GetOrFetchOptionsAsync();

        if (options != null)
            FillAutoComplete(options, field);
    }
}
