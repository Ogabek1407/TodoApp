using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp;

public partial class EditPage : ContentPage
{
    private TodoViewModel _todoView { get; set; }
    public EditPage(TodoViewModel todoView)
    {
        InitializeComponent();
        _todoView = todoView;
        var context = new DataContext();
        var viewModel = new EditPageViewModel(context);
        viewModel.Todo = todoView;
        BindingContext = viewModel;
    }

}