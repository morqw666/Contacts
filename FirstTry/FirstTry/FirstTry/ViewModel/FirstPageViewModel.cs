﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using FirstTry.View;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace FirstTry.ViewModel {
    public class FirstPageViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SignIn { get; private set; }
        public ICommand SignUp { get; private set; }
        public User user { get; private set; }
        public readonly INavigation Navigation;
        public FirstPageViewModel(INavigation navigation) {
            SignIn = new Command(OnSignIn);
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
        public void OnSignIn() {
            string pass = Password;
            string login = Name;
            var userWithLogin = App.GetUserWithLogin(login);
            if (userWithLogin != null) {
                if (userWithLogin.Password == pass) {
                    App.logginedUser = userWithLogin;
                    var page = new MainListView();
                    NavigationPage.SetHasBackButton(page, false);
                    Navigation.PushAsync(page);
                    try {
                        SecureStorage.SetAsync("userKey", login);
                    } catch (Exception) {
                    }
                } else {
                    UserDialogs.Instance.Alert(new AlertConfig() {
                        Title = "Invalid Password",
                        Message = "Make sure password is correct"

                    });
                }
            } else {
                UserDialogs.Instance.Alert(new AlertConfig() {
                    Title = "Invalid Login",
                    Message = "Make sure login is correct"
                });
            }
        }
        public bool LoginAllowed => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password);
        public async void OnSignUp() {
            User user = new User();
            SignUpView signUpView = new SignUpView();
            signUpView.BindingContext = user;
            await Navigation.PushAsync(signUpView);
        }
        protected void OnPropertyChanged(string propName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}
