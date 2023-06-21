using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestWpf.Infrastructure;
using TestWpf.Infrastructure.Base;
using TestWpf.Infrastructure.Services;

namespace TestWpf.ViewModels;

public class AuthorizationViewModel : ViewModelBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthorizationViewModel(INavigationService navigationService, UserManager<IdentityUser> userManager)
    {
        _navigationService = navigationService;
        _userManager = userManager;

        NavigateToHomeCommand = new RelayCommand(e => NavigationService.NavigateTo<HomeViewModel>("Главная"), ce => true);

        LoginCommand = new RelayCommand(async e => await Login(e), canExecute => true);
    }

    private async Task Login(object e)
    {
        var passwordBox = e as PasswordBox;
        
        if (UserName.Length <= 3 || passwordBox.Password.Length <= 3 || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(passwordBox.Password.ToString()))
        {
            MessageBox.Show("Ошибка логина или пароля, введена пустая строка");
            return;
        }

        var user = await _userManager.FindByNameAsync(UserName);

        if (user != null)
        {
            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                UserDataContext.Instance.CurrentUser = user;
                NavigationService.NavigateTo<HomeViewModel>("Home");
                return;
            }

            else
            {
                MessageBox.Show("Вы пользователь");
                return;
            }
        }
        else
        {
            MessageBox.Show("Неправильный логин или пароль");
        }
    }

    public ICommand NavigateToHomeCommand { get; set; }

    public ICommand LoginCommand { get; set; }


    private INavigationService _navigationService;

    public INavigationService NavigationService
    {
        get { return _navigationService; }
        set { _navigationService = value; OnPropertyChanged(); }
    }

    private string _userName;

    public string UserName
    {
        get => _userName;
        set { _userName = value; OnPropertyChanged(); }
    }
}
