using System.Windows.Input;

namespace TodoApp;

public class SimpleCommand : ICommand
{
    private Action Action;

    public event EventHandler CanExecuteChanged;
    public SimpleCommand(Action action)
    {
        Action = action;
    }
    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        Action?.Invoke();
    }
}
