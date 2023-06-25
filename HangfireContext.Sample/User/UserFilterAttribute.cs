using Hangfire.Client;
using Hangfire.Common;

namespace HangfireContext.User;

public class UserFilterAttribute : JobFilterAttribute, IClientFilter
{
    private readonly IUserResolver? _userResolver;

    public UserFilterAttribute(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();
        _userResolver = scope.ServiceProvider.GetService<IUserResolver>();
    }

    public void OnCreating(CreatingContext filterContext)
    {
        var userId = _userResolver?.UserId;

        if (userId.HasValue)
        {
            filterContext.SetJobParameter(JobConstants.User, userId.Value);
        }
    }

    public void OnCreated(CreatedContext filterContext)
    {
    }
}