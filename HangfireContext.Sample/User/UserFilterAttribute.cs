using Hangfire.Client;
using Hangfire.Common;

namespace HangfireContext.User;

public class UserFilterAttribute : JobFilterAttribute, IClientFilter
{
    private readonly IUserResolver? _userResolver;

    public UserFilterAttribute()
    {
    }

    public UserFilterAttribute(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();
        _userResolver = scope.ServiceProvider.GetService<IUserResolver>();
    }

    public void OnCreating(CreatingContext filterContext)
    {
        if (_userResolver is null)
            return;

        var userId = _userResolver.UserId;
        filterContext.SetJobParameter(JobConstants.User, userId);
    }

    public void OnCreated(CreatedContext filterContext)
    {
    }
}