using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public partial class HomePageViewModel : ObservableObject
{
    private readonly DataContext _dataContext;



    private ObservableCollection<TodoViewModel> openToDo;
    public ObservableCollection<TodoViewModel> OpenToDo
    {
        get => openToDo ??= new ObservableCollection<TodoViewModel>();
        set
        {
            openToDo = value;
            OnPropertyChanged(nameof(OpenToDo));
        }
    }



    public long Values2Count => CompletedToDo.Count;



    private ObservableCollection<TodoViewModel> completedToDo;
    public ObservableCollection<TodoViewModel> CompletedToDo
    {
        get => completedToDo ??= new ObservableCollection<TodoViewModel>();
        set
        {
            completedToDo = value;
            OnPropertyChanged(nameof(CompletedToDo));
        }
    }




    public HomePageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
        ReloadTodo();
    }



    public async void ReloadTodo()
    {
        OpenToDo.Clear();
        CompletedToDo.Clear();
        var data = await _dataContext.GetAll();
        var models = await data.ToListAsync();
        foreach (var model in models)
        {
            var item = ConvertViewModel(model);
            OpenToDo.Add(item);
        }
    }



    [RelayCommand]
    public async Task Checked(TodoViewModel todo)
    {
        if (todo.IsChecked)
        {
            todo.IsChecked = false;
            todo.FinishedTime = default;
        }
        else
        {
            todo.IsChecked = true;
            todo.FinishedTime = DateTime.Now;
        }
        var model = ConvertTodoModel(todo);
        await _dataContext.Save(model);
    }



    [RelayCommand]
    public async Task Delete(TodoViewModel todo)
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
