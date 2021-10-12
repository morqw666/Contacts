using SQLite;
using System.Collections.Generic;

namespace FirstTry {
    [Table("Users")]
    public class User {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}