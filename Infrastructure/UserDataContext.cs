using Microsoft.AspNetCore.Identity;

namespace TestWpf.Infrastructure;

public class UserDataContext
{
    private static UserDataContext _instance;

    public static UserDataContext Instance => _instance ??= new UserDataContext();

    private IdentityUser _currentUser;

    public IdentityUser CurrentUser
    {
        get => _currentUser;
        set => _currentUser = value;
    }
}
