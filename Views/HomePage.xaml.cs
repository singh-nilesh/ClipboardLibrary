using clipboardLibrary.ViewModel;

namespace clipboardLibrary.Views;

public partial class HomePage : ContentPage
{
	public HomePage(BooksViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}