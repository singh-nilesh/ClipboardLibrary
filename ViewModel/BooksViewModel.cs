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
        private async Task ShowBookContent(BooksList Book)
        {
            if (Book is null)
                return;
            SelectedBook = Book;
            await Shell.Current.GoToAsync($"{nameof(BookContents)}", true);
        }


        [RelayCommand]
        private async Task RenameBook(BooksList RemBook)
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync("Rename Book", "Enter a New Name");
            if (!string.IsNullOrEmpty(result))
            {
                await _db.RenameBook(RemBook, result);
            }
            await LoadBooks();
        }


        [RelayCommand]
        private async Task DeleteBook(BooksList DelBook)
        {
            await _db.RemoveBook(DelBook);
            await Application.Current.MainPage.DisplayAlert("Book deleted", DelBook.Book, "OK");
            await LoadBooks() ;
        }


    }
}
