using clipboardLibrary.Models;
using clipboardLibrary.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace clipboardLibrary.ViewModel
{
    public partial class ShowBookViewModel : ObservableObject
    {

        [ObservableProperty]
        public BooksList _MyBook;

        public ShowBookViewModel()
        { 
            MyBook = BooksViewModel.SelectedBook;
            Console.WriteLine(MyBook.Book);
        }

        [RelayCommand]
        public async Task DisplayPopup()
        {

            await MauiPopup.PopupAction.DisplayPopup(new PopupPage());
        }
    }
}
