using Microsoft.Extensions.Logging;
using MVRC_Compare.Shared.Services;
using MVRC_Compare.Services;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace MVRC_Compare;

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
            });

        // Add device-specific services used by the MVRC_Compare.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSingleton<ICaseProviderService, WindowsCaseProviderService>();
        builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
