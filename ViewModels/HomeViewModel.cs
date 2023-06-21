using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestWpf.Domain;
using TestWpf.Domain.Entities;
using TestWpf.Infrastructure;
using TestWpf.Infrastructure.Base;
using TestWpf.Infrastructure.Services;

namespace TestWpf.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly DataManager _dataManager;

    public HomeViewModel(INavigationService navigationService, DataManager dataManager)
    {
        _navigationService = navigationService;
        _dataManager = dataManager;

        DeleteAssetCommand = new RelayCommand(async e => await DeleteAssetAsync(e), canExecute => true);

        EditCommand = new RelayCommand(e => MessageBox.Show(e.ToString()), canExecute => true);
        
        LoadDataAsync().ConfigureAwait(false);
    }

    private INavigationService _navigationService;

    public INavigationService NavigationService
    {
        get { return _navigationService; }
        set { _navigationService = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Asset> _assets;

    public ObservableCollection<Asset> Assets
    {
        get { return _assets; }
        set { _assets = value; OnPropertyChanged(); }
    }

    public string CurrentUserName { get; set; } = "Имя пользователя: " + UserDataContext.Instance.CurrentUser.UserName;

    public ICommand EditCommand { get; set; }

    public ICommand DeleteAssetCommand { get; set; }

    private async Task LoadDataAsync()
    {
        Assets = new ObservableCollection<Asset>();

        await foreach(var asset in _dataManager.AsssetsRepository.GetAllAssetsAsync())
            Assets.Add(asset);
    }

    private async Task DeleteAssetAsync(object asset)
    {
        var assetToDelete = asset as Asset;

        var mbox = MessageBox.Show("Вы уверены?", "Удаление объекта", MessageBoxButton.YesNo);

        if (mbox == MessageBoxResult.Yes)
        {
            await _dataManager.AsssetsRepository.DeleteAssetByIdAsync(assetToDelete.AssetId);
            Assets.Remove(assetToDelete);
        }
        else
        {
            return;
        }
    }
}