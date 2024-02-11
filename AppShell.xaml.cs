using clipboardLibrary.Views;

namespace clipboardLibrary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BookContents), typeof(BookContents));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(AutoPage), typeof(AutoPage));
            Routing.RegisterRoute(nameof(PdfPage), typeof(PdfPage));
            Routing.RegisterRoute(nameof(ExportPage), typeof(ExportPage));

        }
    }
}
