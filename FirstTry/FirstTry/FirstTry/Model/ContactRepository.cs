using System.Collections.Generic;
using SQLite;

namespace FirstTry {
    public class ContactRepository {
        SQLiteConnection databaseContact;
        public ContactRepository(string databasePath) {
            databaseContact = new SQLiteConnection(databasePath);
            databaseContact.CreateTable<Contact>();
        }
        public IEnumerable<Contact> GetItems() {
            return databaseContact.Table<Contact>().ToList();
        }
        public Contact GetItem(int id) {
            return databaseContact.Get<Contact>(id);
        }
        public int DeleteItem(int id) {
            return databaseContact.Delete<Contact>(id);
        }
        public int SaveItem(Contact item) {
            if (item.Id != 0) {
                databaseContact.Update(item);
                return item.Id;
            } else {
                return databaseContact.Insert(item);
            }
        }
    }
}