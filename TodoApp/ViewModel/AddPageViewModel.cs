﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using TodoApp.DataAccess;
using TodoApp.DataAccess.Models;

namespace TodoApp.ViewModel;

public partial class AddPageViewModel : ObservableObject
{



    private readonly DataContext _dataContext;



    private string text;
    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged(nameof(Text));
        }
    }



    private DateTime dueDateTask = DateTime.Now;
    public DateTime DueDateTask
    {
        get => dueDateTask;
        set
        {
            dueDateTask = value;
            OnPropertyChanged(nameof(DueDateTask));
        }
    }



    public AddPageViewModel(DataContext dataContext)
    {
        _dataContext = dataContext;
        Text = string.Empty;
        DueDateTask = DateTime.Now;
    }



    public ICommand AddCommand => new SimpleCommand(async () =>
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            await Shell.Current.DisplayAlert("Error", "Enter Task Name: ", "Ok");
            return;
        }
        var todo = new TodoModel()
        {
            Text = Text,
            CreatedTime = DateTime.Now,
            DueDateTime = DueDateTask,
        };
        Text = string.Empty;
        DueDateTask = DateTime.Now;
        await _dataContext.Save(todo);
        await Shell.Current.GoToAsync("//HomePage");
    });



    private TodoModel ConvertTodoModel(TodoViewModel data)
    {
        return new TodoModel()
        {
            CreatedTime = data.CreateTime,
            FinishedTime = (DateTime)data.FinishedTime,
            IsChecked = data.IsChecked,
            Text = data.Text,
            TodoId = data.TodoId,
            UpdatedTime = data.UpdateTime
        };
    }
}
