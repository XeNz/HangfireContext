using Hangfire.Common;
using Hangfire.Server;

namespace HangfireContext.Core.Filter;

[AttributeUsage(AttributeTargets.Class)]
public class HangfirePerformContextAttribute : JobFilterAttribute, IServerFilter
{
    private static PerformContext? _context;
    public static PerformContext PerformContext => new(_context);


    public void OnPerformed(PerformedContext filterContext) => _context = filterContext;

    public void OnPerforming(PerformingContext filterContext) => _context = filterContext;
}