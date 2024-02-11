using SQLite;

namespace clipboardLibrary.Models
{
    public class ClipData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Book { get; set; } 
        public string? Title { get; set; }
        public string? Data { get; set; }
    }
}
