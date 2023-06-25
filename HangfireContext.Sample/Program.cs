using System.Linq.Expressions;
using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using Hangfire.Server;
using Hangfire.States;
using HangfireContext;
using HangfireContext.Core.Manager;
using HangfireContext.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserResolver, UserResolver>();
builder.Services.AddScoped<IMutableUserResolver, UserResolver>();

builder.Services.AddHangfire((sp, hangfireConfig) =>
{
    hangfireConfig
        .UseColouredConsoleLogProvider()
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage()
        .UseFilter(new UserFilterAttribute((sp.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory)!));
});

builder.Services.AddHangfireServer();
builder.Services.AddSingleton<IBackgroundJobManager, HangfireBackgroundJobManager>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("job",
    (IBackgroundJobManager manager, IBackgroundJobClient backgroundJobClient) =>
    {
        // enqueue with own abstraction
        var props = new TestLogJobProps("Log something");
        manager.EnqueueAsync<TestLogJob, TestLogJobProps>(props);
        // enqueue with default hangfire abstraction
        backgroundJobClient.Enqueue<TestLogJob>(job => job.ExecuteAsync(props));
        return Results.Ok();
    }).AddEndpointFilter(async (context, next) =>
{
    var userResolver = context.HttpContext.RequestServices
        .GetRequiredService<IMutableUserResolver>();
    // act as if we need to do some async work to compute the user
    // based on something in HttpContext (User.Identity e.g.)
    await Task.Delay(300);
    userResolver.UserId = Guid.NewGuid();

    return await next(context);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();