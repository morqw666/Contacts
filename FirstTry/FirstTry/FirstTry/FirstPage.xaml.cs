using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstTry {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage {
        public FirstPage() {
            InitializeComponent();
        }
        // обработка нажатия кнопки добавления
        private async void CreateUser(object sender, EventArgs e) {
            User user = new User();
            SignUpView signUpView = new SignUpView();
            signUpView.BindingContext = user;
            await Navigation.PushAsync(signUpView);
        }

    }
}