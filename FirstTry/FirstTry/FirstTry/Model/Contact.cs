using SQLite;

namespace FirstTry {
    [Table("Contacts")]
    public class Contact {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Creator { get; set; }
    }
}
