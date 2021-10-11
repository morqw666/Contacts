using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstTry.ViewModel;

namespace FirstTry.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPageView : ContentPage {
        public FirstPageView() {
            InitializeComponent();
            BindingContext = new FirstPageViewModel(Navigation);
        }
        public void GetLogin(string getLogin) {
            loginEntry.Text = getLogin;
        }

        //по нажатию кнопки вызов метода проверки логина, проверка пароля и переход на MainListView
        //private void ButtonSignIn(object sender, EventArgs e) {
        //    string pass = passwordEntry.Text;
        //    string login = loginEntry.Text;
        //    var userWithLogin = App.GetUserWithLogin(login);
        //    if (userWithLogin != null) {
        //        if (userWithLogin.Password == pass) {
        //            App.logginedUser = userWithLogin;
        //            var page = new MainListView();
        //            NavigationPage.SetHasBackButton(page, false);
        //            Navigation.PushAsync(page);
        //            try {
        //                SecureStorage.SetAsync("userKey", login);
        //            } catch (Exception) {
        //            }
        //        } else {
        //            DisplayAlert("Inavalid password", "Make sure password is correct", "OK");

        //        }
        //    } else {
        //        DisplayAlert("Inavalid login", "Make sure login is correct", "OK");
        //    }
        //}

        //private async void CreateUser(object sender, EventArgs e) {
        //    User user = new User();
        //    SignUpView signUpView = new SignUpView();
        //    signUpView.BindingContext = user;
        //    await Navigation.PushAsync(signUpView);
        //}
    }
}