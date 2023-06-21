using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestWpf.Infrastructure.Base;
using TestWpf.ViewModels;

namespace TestWpf.Infrastructure.Services;

public interface INavigationService
{
    string Title { get; }

    ViewModelBase CurrentView { get; }

    void NavigateTo<TViewModel>(string title) where TViewModel : ViewModelBase;
}

public class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory, UserManager<IdentityUser> userManager)
    {
        _viewModelFactory = viewModelFactory;
        CurrentView = new AuthorizationViewModel(this, userManager);
        Title = "Авторизация";
    }

    private ViewModelBase _currentView;

    public ViewModelBase CurrentView
    {
        get { return _currentView; }
        private set { _currentView = value; OnPropertyChanged(); }
    }

    private string _title;

    public string Title
    {
        get { return _title; }
        private set { _title = value; OnPropertyChanged(); }
    }

    public void NavigateTo<TViewModel>(string title) where TViewModel : ViewModelBase
    {
        CurrentView = _viewModelFactory(typeof(TViewModel));
        Title = title;
    }
}
