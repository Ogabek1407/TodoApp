using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp;

public partial class AddPage : ContentPage
{

    public AddPage(AddPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    public async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var dataContext = new DataContext();
        BindingContext = new AddPageViewModel(dataContext);
    }
}