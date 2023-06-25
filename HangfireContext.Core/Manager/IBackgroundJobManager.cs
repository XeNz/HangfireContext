using HangfireContext.Core.Job;

namespace HangfireContext.Core.Manager;

/// <summary>
/// Defines interface of a job manager.
/// </summary>
public interface IBackgroundJobManager
{
    /// <summary>
    /// Enqueues a job to be executed.
    /// </summary>
    /// <typeparam name="TJob">Type of the job.</typeparam>
    /// <typeparam name="TArgs">Type of the arguments of job.</typeparam>
    /// <param name="args">Job arguments.</param>
    /// <param name="delay">Job delay (wait duration before first try).</param>
    /// <returns>Unique identifier of a background job.</returns>
    Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, TimeSpan? delay = null)
        where TJob : IBackgroundJob<TArgs>;

    /// <summary>
    /// Enqueues a job to be executed.
    /// </summary>
    /// <typeparam name="TJob">Type of the job.</typeparam>
    /// <typeparam name="TArgs">Type of the arguments of job.</typeparam>
    /// <param name="args">Job arguments.</param>
    /// <param name="delay">Job delay (wait duration before first try).</param>
    /// <returns>Unique identifier of a background job.</returns>
    string Enqueue<TJob, TArgs>(TArgs args, TimeSpan? delay = null)
        where TJob : IBackgroundJob<TArgs>;

    /// <summary>
    /// Deletes a job with the specified jobId.
    /// </summary>
    /// <param name="jobId">The Job Unique Identifier.</param>
    /// <returns><c>True</c> on a successfull state transition, <c>false</c> otherwise.</returns>
    Task<bool> DeleteAsync(string jobId);

    /// <summary>
    /// Deletes a job with the specified jobId.
    /// </summary>
    /// <param name="jobId">The Job Unique Identifier.</param>
    /// <returns><c>True</c> on a successful state transition, <c>false</c> otherwise.</returns>
    bool Delete(string jobId);
}