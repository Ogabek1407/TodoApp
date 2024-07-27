using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public class HomePageViewModel : BaseViewModel
{
    private readonly DataContext _dataContext;



    private ObservableCollection<TodoViewModel> values;
    public ObservableCollection<TodoViewModel> Values
    {
        get => values ??= new ObservableCollection<TodoViewModel>();
        set
        {
            values = value;
            OnPropertyChanged(nameof(Values));
        }
    }



    public HomePageViewModel(DataContext dataContext)
    {
        this._dataContext = dataContext;
        ReloadTodo();
    }



    public async void ReloadTodo()
    {
        Values.Clear();
        var data = await _dataContext.GetAll();
        var models = await data.ToListAsync();
        foreach (var model in models)
        {
            var item = ConvertViewModel(model);
            Values.Add(item);
        }
    }



    public ICommand ReloadCommand => new SimpleCommand(async () =>
    {
        ReloadTodo();
    });



    public ICommand EditCommand => new SimpleCommand(async () =>
    {

    });



    public ICommand IsCheckEdCommand => new SimpleCommand(async () =>
    {

    });



    public async Task DeleteTodo(TodoViewModel todo)
    {
        var model = ConvertTodoModel(todo);
        var result = await _dataContext.DeleteTodo(model);
        
        ReloadTodo();
    }



    private TodoModel ConvertTodoModel(TodoViewModel data)
    {
        return new TodoModel()
        {
            CreatedTime = data.CreateTime,
            FinishedTime = data.FinishedTime,
            IsChecked = data.IsChecked,
            DueDateTime = data.DueDateTime,
            Text = data.Text,
            TodoId = data.TodoId,
            UpdatedTime = data.UpdateTime
        };
    }



    private TodoViewModel ConvertViewModel(TodoModel data)
    {
        return new TodoViewModel()
        {
            CreateTime = data.CreatedTime,
            FinishedTime = data.FinishedTime,
            IsChecked = data.IsChecked,
            UpdateTime = data.UpdatedTime,
            DueDateTime = data.DueDateTime,
            TodoId = data.TodoId,
            Text = data.Text,
        };
    }
}
