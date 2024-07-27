using TodoApp.DataAccess;

namespace TodoApp.ViewModel;

public class ProfilePageViewModel : BaseViewModel
{
    private readonly DataContext dataContext;

    public ProfilePageViewModel(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }
}
