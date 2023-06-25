namespace HangfireContext.User;

public class UserResolver : IUserResolver, IMutableUserResolver
{
    private static AsyncLocal<Guid> Id { get; set; } = new();

    public Guid? UserId
    {
        get => Id.Value;
        set
        {
            if (value != null)
            {
                Id.Value = value.Value;
            }
        }
    }
}