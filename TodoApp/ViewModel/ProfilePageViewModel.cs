using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public partial class ProfilePageViewModel : ObservableObject
{
    private readonly DataContext _dataContext;



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



    public ProfilePageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
        ReloadTodo();
    }



    public async void OnChecked2(TodoViewModel todo, bool? data)
    {
        todo.IsChecked = data ??= todo.IsChecked;
        todo.FinishedTime = default;
        var model = ConvertTodoModel(todo);
        await _dataContext.Save(model);
        ReloadTodo();
    }



    public async void ReloadTodo()
    {
        var data = await _dataContext.GetAll();
        var models = await data.ToListAsync();
        values2.Clear();
        foreach (var model in models)
        {
            if (model.IsChecked)
            {
                var item = ConvertViewModel(model);
                Values2.Add(item);
            }
        }
    }



    public async Task DeleteTodo(TodoViewModel todo)
    {
        var model = ConvertTodoModel(todo);
        var result = await _dataContext.DeleteTodo(model);
        ReloadTodo();
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



    private TodoModel ConvertTodoModel(TodoViewModel data)
    {
        return new TodoModel()
        {
            CreatedTime = data.CreateTime,
            FinishedTime = (DateTime)data.FinishedTime,
            IsChecked = data.IsChecked,
            DueDateTime = data.DueDateTime,
            Text = data.Text,
            TodoId = data.TodoId,
            UpdatedTime = data.UpdateTime
        };
    }
}
