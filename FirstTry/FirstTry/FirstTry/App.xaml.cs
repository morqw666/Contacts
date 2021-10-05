﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            MainPage = new NavigationPage( new FirstPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
