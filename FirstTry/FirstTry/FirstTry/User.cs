using SQLite;

namespace FirstTry {
    [Table("Users")]
    public class User {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}