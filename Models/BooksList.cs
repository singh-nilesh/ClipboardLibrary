using SQLite;

namespace clipboardLibrary.Models
{
    public class BooksList
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Book { get; set; }
        public int? ItemCount { get; set; }
    }
}
