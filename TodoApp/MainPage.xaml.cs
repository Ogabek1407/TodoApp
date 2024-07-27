using TodoApp.ViewModel;

namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        private async void onAddTodoPageClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(AddTodoPage));
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var item = button.CommandParameter as TodoViewModel;

            if (item != null)
            {
                _viewModel.DeleteTodo(item);
            }
        }
    }

}
