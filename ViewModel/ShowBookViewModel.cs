using clipboardLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;

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

    }
}
