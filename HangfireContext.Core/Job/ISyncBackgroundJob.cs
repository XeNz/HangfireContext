namespace HangfireContext.Core.Job;

/// <summary>
/// Defines interface of a background job.
/// </summary>
public interface ISyncBackgroundJob<in TArgs> : IBackgroundJob<TArgs>
{
    /// <summary>
    /// Executes the job with the <paramref name="args"/>.
    /// </summary>
    /// <param name="args">Job arguments.</param>
    void Execute(TArgs args);
}