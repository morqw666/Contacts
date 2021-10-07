using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace FirstTry {
    public partial class App : Application {
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
        public App() {
            InitializeComponent();
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
                    MainPage = new NavigationPage(new FirstPage());
                } else {
                    MainPage = new NavigationPage(new MainListView());
                }
            } catch (Exception) {
            }
        }
    }
}
