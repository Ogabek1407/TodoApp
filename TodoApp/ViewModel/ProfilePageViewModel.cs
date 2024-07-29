﻿using System.Collections.ObjectModel;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public class ProfilePageViewModel : BaseViewModel
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



    public ProfilePageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
        ReloadTodo();
    }



    public async void OnChecked(TodoViewModel todo, bool? data)
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
        values.Clear();
        foreach (var model in models)
        {
            if (model.IsChecked)
            {
                var item = ConvertViewModel(model);
                Values.Add(item);
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
            FinishedTime = data.FinishedTime,
            IsChecked = data.IsChecked,
            DueDateTime = data.DueDateTime,
            Text = data.Text,
            TodoId = data.TodoId,
            UpdatedTime = data.UpdateTime
        };
    }
}
