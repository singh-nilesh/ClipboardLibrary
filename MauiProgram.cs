using clipboardLibrary.Database;
using clipboardLibrary.ViewModel;
using clipboardLibrary.Views;
using Microsoft.Extensions.Logging;

namespace clipboardLibrary
{
    public static class MauiProgram
    {
        public static object CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<BooksViewModel>();
            builder.Services.AddSingleton<DbServices>();

            builder.Services.AddTransient<ShowBookViewModel>();
            builder.Services.AddTransient<BookContents>();
            builder.Services.AddTransient<PopupPage>();
            builder.Services.AddTransient<NotesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
