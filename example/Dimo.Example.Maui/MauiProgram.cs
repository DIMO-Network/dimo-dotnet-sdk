using Dimo.Client;
using Dimo.Client.Extensions;
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

        // Add all DIMO services
        // builder.Services.AddDimoClient(options =>
        // {
        //     options.Environment = DimoEnvironment.Production;
        // });
        
        // Add only REST services
        // builder.Services.AddDimoRestServices(options =>
        // {
        //     options.Environment = DimoEnvironment.Production;
        // });

        // Add only graphQL services
        // builder.Services.AddDimoGraphQlServices(options =>
        // {
        //     options.Environment = DimoEnvironment.Production;
        // });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}