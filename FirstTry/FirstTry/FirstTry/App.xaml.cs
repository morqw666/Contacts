using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstTry {
    public partial class App : Application {
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
