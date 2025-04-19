namespace Muslim.Application.Users;

public class CurrentUserHelper : IAutoInjection, ICurrentUser
{

    public int UserId { get { return 0; } }
    public string UserType => throw new NotImplementedException();
    public IEnumerable<string> Permissions => throw new NotImplementedException();
    public string UserName => throw new NotImplementedException();
    public string Email => throw new NotImplementedException();
    public string Phone => throw new NotImplementedException();
    public string DeviceToken => throw new NotImplementedException();
    public string RemoteIpAddress => throw new NotImplementedException();
    public bool IsUserHasPermission(string permission) => throw new NotImplementedException();


}
