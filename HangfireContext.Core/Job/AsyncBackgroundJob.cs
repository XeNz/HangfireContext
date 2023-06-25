namespace HangfireContext.Core.Job;

public abstract class AsyncBackgroundJob<TArgs> : IAsyncBackgroundJob<TArgs>
{
    public abstract Task ExecuteAsync(TArgs args);
}