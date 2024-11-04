using FilmFrameLog.Data;
using FilmFrameLog.Interfaces;
using FilmFrameLog.Pages;
using FilmFrameLog.Services;
using FilmFrameLog.ViewModels;

using Microsoft.Extensions.Logging;

namespace FilmFrameLog;

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
        
        builder.Services.AddSingleton<FilmFrameLogDatabase>();
        builder.Services.AddSingleton<IFireStoreService, FirestoreService>();
        builder.Services.AddSingleton<MyCamerasViewModel>();
        builder.Services.AddSingleton<MyCamerasPage>();
        
        builder.Services.AddSingleton<AddCameraPageViewModel>();
        builder.Services.AddSingleton<AddCameraPage>();
        
        builder.Services.AddSingleton<AddFilmViewModel>();
        builder.Services.AddSingleton<AddFilmPage>();


        
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}