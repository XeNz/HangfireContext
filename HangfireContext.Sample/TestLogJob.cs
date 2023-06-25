using Hangfire.Server;
using HangfireContext.Core.Job;
using HangfireContext.User;

namespace HangfireContext;

public record TestLogJobProps(string ToLog);

public class TestLogJob : AsyncHangfireBackgroundJob<TestLogJobProps>,
    IBackgroundJob<TestLogJobProps>,
    IUserAwareJob
{
    public Guid? User => Context?.GetJobParameter<Guid?>(JobConstants.User);

    protected override Task ExecuteAsync(TestLogJobProps args, PerformContext context)
    {
        Console.WriteLine($"{args.ToLog} done by {User.GetValueOrDefault()}");
        return Task.CompletedTask;
    }

}