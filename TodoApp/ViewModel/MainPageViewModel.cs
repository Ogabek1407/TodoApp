using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public class MainPageViewModel : BaseViewModel
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



    private TodoViewModel todo;
    public TodoViewModel Todo
    {
        get => todo;
        set
        {
            todo = value;
            OnPropertyChanged(nameof(Todo));
        }
    }



    private TodoViewModel editTodo;
    public TodoViewModel EditTodo
    {
        get => editTodo;
        set
        {
            editTodo = value;
            OnPropertyChanged(nameof(EditTodo));
        }
    }



    public MainPageViewModel(DataContext dataContext)
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



    public ICommand AddTodoCommand => new SimpleCommand(async () =>
    {
        if (Todo is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Todo.Text))
        {
            return;
        }

        Todo.CreateTime = DateTime.Now;
        var model = ConvertTodoModel(Todo);
        Todo = new TodoViewModel();
        await _dataContext.Save(model);
    });



    public ICommand EditCommand => new SimpleCommand(async () =>
    {
        if (EditTodo is null)
        {
            return;
        }
        EditTodo.UpdateTime = DateTime.Now;
        var model = ConvertTodoModel(EditTodo);
        EditTodo = new TodoViewModel();
        await _dataContext.Save(model);
    });



    public ICommand IsCheckEdCommand => new SimpleCommand(async () =>
    {

    });



    public ICommand DeleteCommand => new SimpleCommand(async () =>
    {

    });



    private TodoModel ConvertTodoModel(TodoViewModel data)
    {
        return new TodoModel()
        {
            CreatedTime = data.CreateTime,
            FinishedTime = data.FinishedTime,
            IsChecked = data.IsChecked,
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
            TodoId = data.TodoId,
            Text = data.Text,
        };
    }
}
