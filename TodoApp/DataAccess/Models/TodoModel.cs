using SQLite;

namespace TodoApp.DataAccess.Models;

public class TodoModel
{
    [PrimaryKey, AutoIncrement]
    public long TodoId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public DateTime FinishedTime { get; set; }
    public DateTime DueDateTime { get; set; }
    public bool IsChecked { get; set; }
}
