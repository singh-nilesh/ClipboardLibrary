using clipboardLibrary.Database;
using clipboardLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace clipboardLibrary.ViewModel
{
    public partial class BooksViewModel : ObservableObject
    {
        private readonly DbServices _db;
        public BooksViewModel(DbServices services)
        {
            _db = services;
            AllBooks = new ObservableCollection<BooksList>();
        }

        [ObservableProperty]
        private ObservableCollection<BooksList> allBooks;

        [ObservableProperty]
        private BooksList _otpBook;

        [ObservableProperty]
        private string _entryText;

        [ObservableProperty]
        private bool _isBusy;


        [RelayCommand]
        public async Task LoadBooks()
        {
            AllBooks.Clear();
            var FetchBooks = await _db.GetAllBooks();
            if(FetchBooks is not null && FetchBooks.Any())
            {
                foreach(var book in FetchBooks) 
                {
                    AllBooks.Add(book);
                }
            }
        }

        [RelayCommand]
        private async Task AddBook()
        {
            if (string.IsNullOrEmpty(EntryText))
                return;
            var NewBook = new BooksList
            {
                Book = EntryText.ToString(),
                ItemCount = 0
            };
            await _db.AddBook(NewBook);
            await LoadBooks();
            EntryText = string.Empty;
        }


    }
}
