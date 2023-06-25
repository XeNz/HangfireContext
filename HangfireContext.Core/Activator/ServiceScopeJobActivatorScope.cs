using Hangfire;
using Hangfire.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace HangfireContext;

internal class ServiceScopeJobActivatorScope : JobActivatorScope
{
    private readonly IServiceScope _serviceScope;

    public ServiceScopeJobActivatorScope([NotNull] IServiceScope serviceScope) =>
        _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));

    public override object Resolve(Type type) =>
        ActivatorUtilities.GetServiceOrCreateInstance(_serviceScope.ServiceProvider, type);

    public override void DisposeScope()
    {
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1
        if (_serviceScope is IAsyncDisposable asyncDisposable)
        {
            // Service scope disposal is triggered inside a dedicated background thread,
            // while Task result is being set in CLR's Thread Pool, so no deadlocks on
            // wait should happen.
            asyncDisposable.DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return;
        }
#endif
        _serviceScope.Dispose();
    }
}