using clipboardLibrary.Views;

namespace clipboardLibrary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BookContents), typeof(BookContents));
            Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage));

        }
    }
}
