using clipboardLibrary.Models;
using SQLite;
using System.Diagnostics;

namespace clipboardLibrary.Database
{
    public class DbServices
    {
        private const string DbName = "ClipLibrary.db3";
        private static string DbPath = Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection? _connection;
        public SQLiteAsyncConnection Db =>
            (_connection ??= new SQLiteAsyncConnection(DbPath));

        public async Task Init()
        {
            await Db.CreateTableAsync<ClipData>();
            await Db.CreateTableAsync<BooksList>();
        }


        // Operations on ClipData
        public async Task AddClip(ClipData Note)
        {
            await Init();

            var clip = new ClipData
            {
                Book = Note.Book,
                Title = Note.Title,
                Data = Note.Data,
            };
            await Db.InsertAsync(clip)
                .ContinueWith(t => { Debug.WriteLine("new clip added id = {0}", clip.Id); });

            // updating the bookitem count.
            var count = await Db.Table<BooksList>().Where(b => b.Book.Equals(Note.Book)).CountAsync();
            BooksList t_book = new()
            {
                Book = Note.Book,
                ItemCount = count
            };
            await Db.UpdateAsync(t_book);
        }

        public async Task RemoveClip(ClipData note)
        {
            await Init();
            await Db.DeleteAsync<ClipData>(note);

            // updating the bookitem count.
            var count = await Db.Table<BooksList>().Where(b => b.Book.Equals(note.Book)).CountAsync();
            BooksList t_book = new BooksList
            {
                Book = note.Book,
                ItemCount = count
            };
            await Db.UpdateAsync(t_book);

        }

        public async Task<IEnumerable<ClipData>> GetBook(string BookName)
        {
            await Init();
            return await Db.QueryAsync<ClipData>("select * from ClipData where Book = ?", BookName);
        }

        public async Task<List<ClipData>> GetAllNotes()
        {
            await Init();
            return await Db.Table<ClipData>().ToListAsync();
        }

        public async Task RemoveBook(BooksList DelBook)
        {
            await Init();
            await Db.QueryAsync<ClipData>("delete * from ClipData where Book = ?", DelBook.Book)
                .ContinueWith(t => { Debug.WriteLine("deleted Book " + DelBook.Book); });
            
            await Db.DeleteAsync<BooksList>(DelBook.Id);
        }

        public async Task<bool> UpdateClip(ClipData newData)
        {
            await Init();
            return await Db.UpdateAsync(newData) > 0;
        }



        // operations on Booklist
        public async Task<List<BooksList>> GetAllBooks()
        {
            await Init();
            return await Db.Table<BooksList>().ToListAsync();
        }

        public async Task AddBook(BooksList book)
        {
            await Init();
            await Db.InsertAsync(book);
        }

        public async Task RenameBook(BooksList RenmBook, string NewName)
        {
            await Init();
            var notes = await Db.QueryAsync<ClipData>("select * from ClipData where Book = ?", RenmBook.Book);

            foreach (var item in notes)
            {
                item.Book = NewName;
            }
            await Db.UpdateAllAsync(notes);

            RenmBook.Book = NewName;
            await Db.UpdateAsync(RenmBook);
        }

    }
}
