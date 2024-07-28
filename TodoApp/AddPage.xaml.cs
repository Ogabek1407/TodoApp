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
}