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
        }

        public async Task<bool> RemoveClip(int id)
        {
            await Init();
            return await Db.DeleteAsync<ClipData>(id) > 0;
        }

        public async Task<IEnumerable<ClipData>> GetBook(string BookName)
        {
            await Init();
            return await Db.QueryAsync<ClipData>("select * from ClipData where Book = ?", BookName);
        }

        public async Task RemoveBook(string BookName)
        {
            await Init();
            await Db.QueryAsync<ClipData>("delete * from ClipData where Book = ?", BookName)
                .ContinueWith(t => { Debug.WriteLine("deleted Book " + BookName); });
        }

        public async Task<bool> UpdateClip(ClipData newData)
        {
            await Init();
            return await Db.UpdateAsync(newData) > 0;
        }

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

    }
}
