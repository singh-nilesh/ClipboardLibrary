using clipboardLibrary.ViewModel;

namespace clipboardLibrary.Views;

public partial class BookContents : ContentPage
{
	public BookContents()
	{
		InitializeComponent();
		BindingContext = new ShowBookViewModel();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

}