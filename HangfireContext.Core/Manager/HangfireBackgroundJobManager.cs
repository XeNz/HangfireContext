using HangfireContext.Core.Job;

namespace HangfireContext.Core.Manager;

using HangfireBackgroundJob = Hangfire.BackgroundJob;

public class HangfireBackgroundJobManager : IBackgroundJobManager
{
    public virtual Task<string> EnqueueAsync<TJob, TArgs>(TArgs args,
        TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
    {
        var jobUniqueIdentifier = string.Empty;

        if (!delay.HasValue)
        {
            jobUniqueIdentifier = typeof(ISyncBackgroundJob<TArgs>).IsAssignableFrom(typeof(TJob))
                ? HangfireBackgroundJob.Enqueue<TJob>(job => ((ISyncBackgroundJob<TArgs>)job).Execute(args))
                : HangfireBackgroundJob.Enqueue<TJob>(job => ((IAsyncBackgroundJob<TArgs>)job).ExecuteAsync(args));
        }
        else
        {
            jobUniqueIdentifier = typeof(ISyncBackgroundJob<TArgs>).IsAssignableFrom(typeof(TJob))
                ? HangfireBackgroundJob.Schedule<TJob>(job => ((ISyncBackgroundJob<TArgs>)job).Execute(args), delay.Value)
                : HangfireBackgroundJob.Schedule<TJob>(job => ((IAsyncBackgroundJob<TArgs>)job).ExecuteAsync(args), delay.Value);
        }

        return Task.FromResult(jobUniqueIdentifier);
    }


    public virtual string Enqueue<TJob, TArgs>(TArgs args, TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
    {
        var jobUniqueIdentifier = string.Empty;

        if (!delay.HasValue)
        {
            jobUniqueIdentifier = typeof(ISyncBackgroundJob<TArgs>).IsAssignableFrom(typeof(TJob))
                ? HangfireBackgroundJob.Enqueue<TJob>(job => ((ISyncBackgroundJob<TArgs>)job).Execute(args))
                : HangfireBackgroundJob.Enqueue<TJob>(job => ((IAsyncBackgroundJob<TArgs>)job).ExecuteAsync(args));
        }
        else
        {
            jobUniqueIdentifier = typeof(ISyncBackgroundJob<TArgs>).IsAssignableFrom(typeof(TJob))
                ? HangfireBackgroundJob.Schedule<TJob>(job => ((ISyncBackgroundJob<TArgs>)job).Execute(args), delay.Value)
                : HangfireBackgroundJob.Schedule<TJob>(job => ((IAsyncBackgroundJob<TArgs>)job).ExecuteAsync(args), delay.Value);
        }

        return jobUniqueIdentifier;
    }

    public virtual Task<bool> DeleteAsync(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            throw new ArgumentNullException(nameof(jobId));
        }

        bool successfulDeletion = HangfireBackgroundJob.Delete(jobId);
        return Task.FromResult(successfulDeletion);
    }

    public virtual bool Delete(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            throw new ArgumentNullException(nameof(jobId));
        }

        bool successfulDeletion = HangfireBackgroundJob.Delete(jobId);
        return successfulDeletion;
    }
}