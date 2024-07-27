using SQLite;
using TodoApp.DataAccess.Models;

namespace TodoApp.DataAccess;

public class DataContext
{
    private SQLiteAsyncConnection _database;
    private async Task Init()
    {
        if (_database is not null)
        {
            return;
        }
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<TodoModel>();
    }


    public async Task<AsyncTableQuery<TodoModel>> GetAll()
    {
        await Init();
        return _database.Table<TodoModel>();
    }

    public async Task<TodoModel> GetById(long TodoId)
    {
        await Init();
        var data = await _database.Table<TodoModel>().ToListAsync();
        var item = data.Where(x => x.TodoId == TodoId).FirstOrDefault();
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        return item;
    }

    public async Task<int> Save(TodoModel item)
    {
        await Init();

        if (item.TodoId == 0)
        {
            return await _database.InsertAsync(item);
        }
        else
        {
            return await _database.UpdateAsync(item);
        }
    }

    public async Task<int> DeleteTodo(TodoModel item)
    {
        await Init();
        var result = await _database.DeleteAsync(item);
        return result;
    }
}