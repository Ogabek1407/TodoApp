using System.Windows.Input;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public class EditPageViewModel : BaseViewModel
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



    public DateTime TodayDate => DateTime.Today;



    public EditPageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
    }



    public ICommand UpdateCommand => new SimpleCommand(async () =>
    {
        if (string.IsNullOrWhiteSpace(Todo.Text))
        {
            return;
        }
        if (Todo.DueDateTime < DateTime.Today)
        {
            return;
        }
        Todo.UpdateTime = DateTime.Now;
        var model = ConvertTodoModel(Todo);
        await _dataContext.Save(model);
        await Shell.Current.GoToAsync("//HomePage");
    });



    private TodoModel ConvertTodoModel(TodoViewModel data)
    {
        return new TodoModel()
        {
            CreatedTime = data.CreateTime,
            FinishedTime = data.FinishedTime,
            IsChecked = data.IsChecked,
            Text = data.Text,
            DueDateTime = data.DueDateTime,
            TodoId = data.TodoId,
            UpdatedTime = data.UpdateTime
        };
    }
}
