namespace TodoApp.ViewModel;

public class TodoViewModel : BaseViewModel
{
    private long todoId;
    public long TodoId
    {
        get => todoId;
        set
        {
            todoId = value;
            OnPropertyChanged(nameof(TodoId));
        }
    }
    private string text;
    public string Text
    {
        get => text;
        set
        {
            text = value; OnPropertyChanged(Text);
        }
    }
    private DateTime createdTime;
    public DateTime CreateTime
    {
        get => createdTime;
        set
        {
            createdTime = value;
            OnPropertyChanged(nameof(CreateTime));
        }
    }
    private DateTime updatedTime;
    public DateTime UpdateTime
    {
        get => updatedTime;
        set
        {
            updatedTime = value;
            OnPropertyChanged(nameof(UpdateTime));
        }
    }
    private DateTime finishedTime;
    public DateTime FinishedTime
    {
        get => finishedTime;
        set
        {
            finishedTime = value;
            OnPropertyChanged(nameof(FinishedTime));
        }
    }
    private DateTime dueDateTime;
    public DateTime DueDateTime
    {
        get => dueDateTime;
        set
        {
            dueDateTime = value;
            OnPropertyChanged(nameof(DueDateTime));
        }
    }
    private bool isCompleted;
    public bool IsChecked
    {
        get => isCompleted;
        set
        {
            isCompleted = value;
            OnPropertyChanged(nameof(IsChecked));
        }
    }
}
