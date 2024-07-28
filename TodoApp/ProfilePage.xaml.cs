using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp;

public partial class ProfilePage : ContentPage
{
    private readonly DataContext _dataContext;
    private ProfilePageViewModel viewModel;

    public ProfilePage(DataContext dataContext)
    {
        InitializeComponent();
        _dataContext = dataContext;
        viewModel = new ProfilePageViewModel(_dataContext);
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel = new ProfilePageViewModel(_dataContext);
        BindingContext = viewModel;
    }
}