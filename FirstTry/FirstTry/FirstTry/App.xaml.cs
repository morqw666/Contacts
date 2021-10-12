using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Linq;

namespace FirstTry {
    public partial class App : Application {
        public static User logginedUser;
        public const string DATABASE_NAME = "users.db";
        public static UserRepository database;
        public static UserRepository Database {
            get {
                if (database == null) {
                    database = new UserRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public const string DATABASE_CONTACT = "contacts.db";
        public static ContactRepository databaseContact;
        public static ContactRepository DatabaseContact {
            get {
                if (databaseContact == null) {
                    databaseContact = new ContactRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_CONTACT));
                }
                return databaseContact;
            }
        }
        public App() {
            InitializeComponent();
            //MainPage = new NavigationPage(new View.SignUpView());
            LoadFirstPage();
            //MainPage = new NavigationPage( new FirstPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }

        //метод проверки ключа залогиненого юзера
        private async void LoadFirstPage() {
            try {
                var test = SecureStorage.GetAsync("userKey");
                await test;
                if (test.Result == null) {
                    MainPage = new NavigationPage(new View.FirstPageView());
                } else {
                    logginedUser = GetUserWithLogin(test.Result);
                    MainPage = new NavigationPage(new View.MainListView());
                }
            } catch (Exception) {
            }
        }
        public static User GetUserWithLogin(string login) {
            var users = Database.GetItems();
            for (int i = 0; i < users.Count(); i++) {
                var user = users.ElementAt(i);
                if (user.Name == login)
                    return user;
            }
            return null;
        }
    }
}
