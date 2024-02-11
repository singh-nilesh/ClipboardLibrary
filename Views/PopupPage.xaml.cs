using clipboardLibrary.Models;
using clipboardLibrary.ViewModel;
using MauiPopup.Views;

namespace clipboardLibrary.Views;

public partial class PopupPage : BasePopupPage
{
	public PopupPage()
	{
		InitializeComponent();
	}

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        MauiPopup.PopupAction.ClosePopup();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var Notes = new ClipData
        {
            Book = this.BookName.Text.ToString(),
            Title = this.NotesTitel.Text.ToString(),
            Data = this.NotesBody.Text.ToString()
        };
        ShowBookViewModel.NewNotes = Notes;
    }
}