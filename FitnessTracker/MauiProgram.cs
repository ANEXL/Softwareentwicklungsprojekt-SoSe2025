using FitnessTracker.Services;
using FitnessTracker;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace FitnessTracker;

public static class MauiProgram
{
    public static MauiApp Current { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<DatabaseService>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<WorkoutPage>();

        Current = builder.Build();
        return Current;
    }
}