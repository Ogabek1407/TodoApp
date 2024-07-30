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



    private async void OnFrameTapped(object sender, EventArgs e)
    {
        //await DisplayAlert("Tapped", "You tapped the frame!", "OK");
        var frame = sender as Frame;

        var todo = frame?.BindingContext as TodoViewModel;
        if (todo != null)
        {
            await Navigation.PushAsync(new EditPage(todo));
        }
    }



    public void OnCheckBoxChecked2Changed(object sender, EventArgs e)
    {
        var checkBox = sender as CheckBox;
        var todo = checkBox?.BindingContext as TodoViewModel;
        var data = checkBox?.IsChecked;
        if (todo != null && data == false)
        {
            viewModel.OnChecked2(todo, data);
        }
    }



    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        var item = button.CommandParameter as TodoViewModel;

        if (item != null)
        {
            await viewModel.DeleteTodo(item);
        }
    }

    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }
}