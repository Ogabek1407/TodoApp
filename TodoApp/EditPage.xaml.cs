using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp;

public partial class EditPage : ContentPage
{
    private TodoViewModel _todoView { get; set; }
    private EditPageViewModel viewModel { get; set; }
    public EditPage(TodoViewModel todoView)
    {
        InitializeComponent();
        _todoView = todoView;
        var context = new DataContext();
        viewModel = new EditPageViewModel(context);
        viewModel.Todo = todoView;
        BindingContext = viewModel;
    }

    public async void SaveClicked(object sender, EventArgs e)
    {
        viewModel.Save();
        await Navigation.PopAsync();
    }
}