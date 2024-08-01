using CommunityToolkit.Mvvm.ComponentModel;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public partial class EditPageViewModel : ObservableObject
{



    private readonly DataContext _dataContext;



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



    public EditPageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
    }



    public async Task<bool> Save()
    {
        if (string.IsNullOrWhiteSpace(Todo.Text))
        {
            return false;
        }
        Todo.UpdateTime = DateTime.Now;
        var model = ConvertTodoModel(Todo);
        await _dataContext.Save(model);
        return true;
    }



    private TodoModel ConvertTodoModel(TodoViewModel data)
    {
        return new TodoModel()
        {
            CreatedTime = data.CreateTime,
            FinishedTime = (DateTime)data.FinishedTime,
            IsChecked = data.IsChecked,
            Text = data.Text,
            DueDateTime = data.DueDateTime,
            TodoId = data.TodoId,
            UpdatedTime = data.UpdateTime
        };
    }
}
