using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using FirstTry.View;
using Xamarin.Essentials;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using System.Linq;

namespace FirstTry.ViewModel {
    public class SignUpViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SignUp { get; private set; }
        public User user { get; private set; }
        public readonly INavigation Navigation;
        public SignUpViewModel(INavigation navigation) {
            SignUp = new Command(OnSignUp);
            user = new User();
            Navigation = navigation;
        }
        public string Name {
            get { return user.Name; }
            set {
                if (user.Name != value) {
                    user.Name = value;
                    OnPropertyChanged("Name");
                    OnPropertyChanged(nameof(LoginAllowed));
                }
            }
        }
        public string Password {
            get { return user.Password; }
            set {
                if (user.Password != value) {
                    user.Password = value;
                    OnPropertyChanged("Password");
                    OnPropertyChanged(nameof(LoginAllowed));
                }
            }
        }
        public string ConfirmPassword {
            get { return user.ConfirmPassword; }
            set {
                if (user.ConfirmPassword != value) {
                    user.ConfirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
                    OnPropertyChanged(nameof(LoginAllowed));
                }
            }
        }
        public void OnSignUp() {
            string login = Name;
            string pass = Password;
            string confPass = ConfirmPassword;
            Regex passReg = new Regex("(?=.*[@#$!\"&])(?=.*[A-Z]).*\\d.*");
            Match match = passReg.Match(pass);
            Regex loginReg = new Regex(@"^\d+");
            Match match2 = loginReg.Match(login);

            CheckLoginExists(login);

            if (login.Length > 3 && login.Length < 17) {
                if (!match2.Success) {
                    if (pass.Length > 7 && pass.Length < 17) {
                        if (match.Success) {
                            if (pass == confPass) {
                                if (CheckLoginExists(login)) {
                                    UserDialogs.Instance.Alert(new AlertConfig() {
                                        Title = "Ivalid login",
                                        Message = "Please choose another one"
                                    });
                                } else {
                                    App.Database.SaveItem(user);
                                    var page = new FirstPageView();
                                    page.GetLogin(login);
                                    NavigationPage.SetHasBackButton(page, false);
                                    Navigation.PushAsync(page);
                                }
                            } else {
                                UserDialogs.Instance.Alert(new AlertConfig() {
                                    Title = "Alert",
                                    Message = "Please make sure your password match"
                                });
                            }
                        } else {
                            UserDialogs.Instance.Alert(new AlertConfig() {
                                Title = "Invalid password",
                                Message = "Password must contain at least one uppercase letter, one number and one special symbol"
                            });
                        }
                    } else {
                        UserDialogs.Instance.Alert(new AlertConfig() {
                            Title = "Invalid password",
                            Message = "Password be at least 8 and no more then 16"
                        });
                    }
                } else {
                    UserDialogs.Instance.Alert(new AlertConfig() {
                        Title = "Invalid login",
                        Message = "Login start should not contain numbers"
                    });
                }
            } else {
                UserDialogs.Instance.Alert(new AlertConfig() {
                    Title = "Invalid login",
                    Message = "Login be at least 4 and no more then 16"
                });
            }
        }
        //метод проверки логина
        private bool CheckLoginExists(string login) {
            var items = App.Database.GetItems();
            for (int i = 0; i < items.Count(); i++) {
                User user = items.ElementAt(i);
                if (user.Name == login) {
                    return true;
                }
            }
            return false;
        }
        public bool LoginAllowed => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
        protected void OnPropertyChanged(string propName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
