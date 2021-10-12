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
    public partial class SignUpView : ContentPage {
        public SignUpView() {
            InitializeComponent();
            BindingContext = new SignUpViewModel(Navigation);
        } 
    //    private void SaveUser(object sender, EventArgs e) {
    //        var user = (User)BindingContext;
    //        string login = loginEntry.Text;
    //        string pass = passwordEntry.Text;
    //        string confPass = confirmPasswordEntry.Text;
    //        Regex passReg = new Regex("(?=.*[@#$!\"&])(?=.*[A-Z]).*\\d.*");
    //        Match match = passReg.Match(pass);
    //        Regex loginReg = new Regex(@"^\d+");
    //        Match match2 = loginReg.Match(login);

    //        CheckLoginExists(login);

    //        if (login.Length > 3 && login.Length <17) {
    //            if (!match2.Success) {
    //                if (pass.Length > 7 && pass.Length < 17) {
    //                    if (match.Success) {
    //                        if (pass == confPass) {
    //                            if (CheckLoginExists(login)) {
    //                                DisplayAlert("Ivalid login", "Please choose another one", "OK");
    //                            } else {
    //                                App.Database.SaveItem(user);
    //                                var page = new FirstPageView();
    //                                page.GetLogin(login);
    //                                NavigationPage.SetHasBackButton(page, false);
    //                                Navigation.PushAsync(page);
    //                            }
    //                        } else {
    //                            DisplayAlert("Alert", "Please make sure your password match", "OK");
    //                        }
    //                    } else {
    //                        DisplayAlert("Invalid password", "Password must contain at least one uppercase letter, one number and one special symbol", "OK");
    //                    }
    //                } else {
    //                    DisplayAlert("Invalid password", "Password be at least 8 and no more then 16", "OK");
    //                }
    //            } else {
    //                DisplayAlert("Invalid login", "Login start should not contain numbers", "OK");
    //            }
    //        } else {
    //            DisplayAlert("Invalid login", "Login be at least 4 and no more then 16", "OK");
    //        }     
    //    }
    //    //метод проверки логина
    //    private bool CheckLoginExists(string login) {
    //        var items = App.Database.GetItems();
    //        for (int i = 0; i < items.Count(); i++) {
    //            User user = items.ElementAt(i);
    //            if ( user.Name == login) {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    }
}
