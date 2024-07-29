namespace TodoApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage),typeof(HomePage));
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddPage)}");
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
        }
    }
}