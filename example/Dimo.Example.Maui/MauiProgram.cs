using Dimo.Client;
using Dimo.Client.Core;
using Microsoft.Extensions.Logging;

namespace Dimo.Example.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<MainPage>();

        builder.Services.AddDimoClient(DimoEnvironment.Development);
        //builder.Services.AddCoreServices(DimoEnvironment.Development);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}