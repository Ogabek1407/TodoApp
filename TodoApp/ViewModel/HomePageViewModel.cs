using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public class HomePageViewModel : BaseViewModel
{
    private readonly DataContext _dataContext;



    public long ValuesCount => Values.Count;



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



    public long Values2Count => Values2.Count;



    private ObservableCollection<TodoViewModel> values2;
    public ObservableCollection<TodoViewModel> Values2
    {
        get => values2 ??= new ObservableCollection<TodoViewModel>();
        set
        {
            values2 = value;
            OnPropertyChanged(nameof(Values2));
        }
    }




    public HomePageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
        ReloadTodo();
    }



    public async void ReloadTodo()
    {
        Values.Clear();
        Values2.Clear();
        var data = await _dataContext.GetAll();
        var models = await data.ToListAsync();
        foreach (var model in models)
        {
            //if (!model.IsChecked)
            //{
            var item = ConvertViewModel(model);
            Values.Add(item);
            //}
            //else
            //{
            //    Values2.Add(ConvertViewModel(model));
            //}
        }
    }



    public bool CheckDate(DateTime date)
    {
        if (date < DateTime.Now)
        {
            return false;
        }
        return true;
    }



    public async void OnChecked(TodoViewModel todo, bool? data)
    {
        todo.IsChecked = data ??= !todo.IsChecked;
        todo.FinishedTime = DateTime.Now;
        var model = ConvertTodoModel(todo);
        await _dataContext.Save(model);
        return;
    }



    public async void OnChecked2(TodoViewModel todo, bool? data)
    {
        todo.IsChecked = data ??= todo.IsChecked;
        todo.FinishedTime = default;
        var model = ConvertTodoModel(todo);
        await _dataContext.Save(model);
        ReloadTodo();
    }



    public async Task DeleteTodo(TodoViewModel todo)
    {
        var model = ConvertTodoModel(todo);
        var result = await _dataContext.DeleteTodo(model);
        ReloadTodo();
    }



    //private async Task PlayCompletionSound()
    //{
    //    var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("complete_task.wav"));
    //    player.Play();
    //}



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
