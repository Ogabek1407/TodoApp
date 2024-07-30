using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp;

public partial class HomePage : ContentPage
{



    private HomePageViewModel viewModel;



    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }



    public void OnCheckBoxCheckedChanged(object sender, EventArgs e)
    {
        var checkBox = sender as CheckBox;
        var todo = checkBox?.BindingContext as TodoViewModel;
        var data = checkBox?.IsChecked;
        if (todo != null)
        {
            viewModel.OnChecked(todo, data);
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



    protected override void OnAppearing()
    {
        base.OnAppearing();
        var data = new DataContext();
        viewModel = new HomePageViewModel(data);
        BindingContext = viewModel;
    }



    public bool CheckDate(DateTime date)
    {
        if (date < DateTime.Now)
        {
            return false;
        }
        return true;
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
}