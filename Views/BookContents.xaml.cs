using clipboardLibrary.ViewModel;

namespace clipboardLibrary.Views;

public partial class BookContents : ContentPage
{
	private ShowBookViewModel _vm;
	public BookContents(ShowBookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.LoadNotes();
	}
}