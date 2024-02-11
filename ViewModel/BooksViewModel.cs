using clipboardLibrary.Database;
using clipboardLibrary.Models;
using clipboardLibrary.Views;
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
        private string? _entryText;


        [RelayCommand]
        public async Task LoadBooks()
        {
            AllBooks.Clear();
            if(string.IsNullOrEmpty(EntryText))
            {
                var FetchBooks = await _db.GetAllBooks();
                if (FetchBooks is not null && FetchBooks.Any())
                {
                    foreach (var book in FetchBooks)
                    {
                        AllBooks.Add(book);
                    }
                }
            }
            else
            {
                var FetchBooks = await _db.GetAllBooks();
                if (FetchBooks is not null && FetchBooks.Any())
                {
                    foreach (var book in FetchBooks.Where(b => b.Book.ToLower().Contains(EntryText.ToLower())))
                    {
                        AllBooks.Add(book);
                    }
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
            EntryText = string.Empty;
        }


        public static BooksList? SelectedBook;

        [RelayCommand]
        async Task ShowBookContent(BooksList book)
        {
            if (book is null)
                return;
            SelectedBook = book;
            await Shell.Current.GoToAsync($"{nameof(BookContents)}", true);
        }

        
    }
}
