using clipboardLibrary.Database;
using clipboardLibrary.Models;
using clipboardLibrary.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace clipboardLibrary.ViewModel
{
    public partial class ShowBookViewModel : ObservableObject
    {
        // Constructor
        public ShowBookViewModel(DbServices services)
        {
            _db = services;
            MyBook = BooksViewModel.SelectedBook;
            Debug.WriteLine(MyBook.Book);
            AllNotes = new ObservableCollection<ClipData>();
        }


        // Variable Declarations

        private readonly DbServices _db;

        [ObservableProperty]
        public BooksList _MyBook; // selected book

        public static ClipData? NewNotes; // for storing data from Popup

        [ObservableProperty]
        private ObservableCollection<ClipData> _allNotes;

        [ObservableProperty]
        private string? _searchEntry;




        // Methord Declarations

        [RelayCommand]
        public async Task AddNotes()
        {

            await MauiPopup.PopupAction.DisplayPopup(new PopupPage());
            if (NewNotes is null) return;

            NewNotes.Book = MyBook.Book;
            await _db.AddClip(NewNotes);
            NewNotes = null;
            await LoadNotes();
        }



        [RelayCommand]
        public async Task LoadNotes()
        {
            AllNotes.Clear();
            if (string.IsNullOrEmpty(SearchEntry))
            {
                var FetchNotes = await _db.GetBook(MyBook.Book);
                if (FetchNotes is not null && FetchNotes.Any())
                {
                    foreach (var note in FetchNotes)
                    {
                        AllNotes.Add(note);
                    }
                }
            }
            else
            {
                var FetchNotes = await _db.GetBook(MyBook.Book);
                if (FetchNotes is not null && FetchNotes.Any())
                {
                    foreach (var note in FetchNotes.Where(n => n.Data.ToLower().Contains(SearchEntry.ToLower())))
                    {
                        AllNotes.Add(note);
                    }
                }
            }
        }

        [RelayCommand]
        private async Task DeleteNote(ClipData DelNotes)
        {
            await _db.RemoveClip(DelNotes);
            await Application.Current.MainPage.DisplayAlert("Note Deleated", DelNotes.Title, "OK");
            await LoadNotes();
        }

        [RelayCommand]
        private async Task ShowNotes(ClipData EditNote)
        {
            if (EditNote is null)
                return;
            await Shell.Current.GoToAsync($"{nameof(NotesPage)}", true);
        }

    }
}
