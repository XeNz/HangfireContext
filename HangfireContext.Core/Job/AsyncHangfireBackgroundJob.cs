using Hangfire.Server;
using HangfireContext.Core.Filter;

namespace HangfireContext.Core.Job;

[HangfirePerformContext]
public abstract class AsyncHangfireBackgroundJob<TArgs> : AsyncBackgroundJob<TArgs>
{
    public PerformContext? Context { get; set; }

    public override async Task ExecuteAsync(TArgs args)
    {
        Context = HangfirePerformContextAttribute.PerformContext;
        await ExecuteAsync(args, Context);
    }

    protected abstract Task ExecuteAsync(TArgs args, PerformContext context);
}