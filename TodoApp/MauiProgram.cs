using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TodoApp.DataAccess;
using TodoApp.ViewModel;

namespace TodoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DataContext>();

            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<AddPageViewModel>();
            builder.Services.AddTransient<ProfilePageViewModel>();



            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<ProfilePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
