namespace HangfireContext.User;

public interface IUserAwareJob
{
    Guid? User { get; }
}