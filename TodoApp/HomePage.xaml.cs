using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp;

public partial class HomePage : ContentPage
{
    private bool _isVisible = true;

    public bool isVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            if (!_isVisible)
            {
                IsVisible();
            }
        }
    }

    private HomePageViewModel viewModel;

    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnFrameTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var todo = frame?.BindingContext as TodoViewModel;

        if (todo != null && isVisible)
        {
            isVisible = false;
            await Navigation.PushAsync(new EditPage(todo));
        }
    }

    private async void IsVisible()
    {
        await Task.Delay(1000);
        isVisible = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var data = new DataContext();
        viewModel = new HomePageViewModel(data);
        BindingContext = viewModel;
    }

    public bool CheckDate(DateTime date)
    {
        return date >= DateTime.Now;
    }
}