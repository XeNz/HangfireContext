namespace HangfireContext.User;

public interface IMutableUserResolver
{
    Guid? UserId { set; }
}