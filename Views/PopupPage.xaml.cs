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

    private void Submit_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(this.NotesBody.Text)) 
        {
            this.lblWarning.IsVisible = true;
        }
        else
        {
            this.lblWarning.IsVisible = false;
            var Notes = new ClipData
            {
                Book = string.Empty,
                Title = this.NotesTitel.Text.ToString(),
                Data = this.NotesBody.Text.ToString()
            };
            ShowBookViewModel.NewNotes = Notes;
            MauiPopup.PopupAction.ClosePopup();
        }
    }
}