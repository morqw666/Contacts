﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;

namespace FirstTry {    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage {
        //public ICommand MenuItemDeleteCommand { get; set; }
        public MainListView() {
            InitializeComponent();
            //MenuItemDeleteCommand = new Command((object contactObj) => {
            //    Contact contact = contactObj as Contact;
            //    DisplayAlert("Info",contact.Id.ToString(),"OK");
            //    if (contact == null) return;
            //    App.DatabaseContact.DeleteItem(contact.Id);
            //});
        }

        //public ICommand MenuItemDeleteCommand => new Command(MenuItemDelete);
        //private void MenuItemDelete(object contactObj) {
        //    Contact contact = contactObj as Contact;
        //    DisplayAlert("Info",contact.Id.ToString(),"OK");
        //    if (contact == null) return;
        //    App.DatabaseContact.DeleteItem(contact.Id);
        //}
        private void OnClickDelProfile(object sender, EventArgs e) {
            Contact contact = sender as Contact;
            if (contact == null) return;
            App.DatabaseContact.DeleteItem(contact.Id);
        }
        private async void OnClickAddProfile(object sender, EventArgs e) {
            Contact contact = new Contact();
            AddEditProfileView addEditProfileView = new AddEditProfileView();
            addEditProfileView.BindingContext = contact;
            await Navigation.PushAsync(addEditProfileView);
        }
        private void OnClickLogOut(object sender, EventArgs e) {
            SecureStorage.Remove("userKey");
            var page = new FirstPage();
            NavigationPage.SetHasBackButton(page, false);
            Navigation.PushAsync(page);

        }
        protected override void OnAppearing() {
            base.OnAppearing();
            var listView = App.DatabaseContact.GetItems().ToList();
            for (int i = listView.Count() - 1; i >= 0; i--) {
                var contact = listView.ElementAt(i);
                if (contact.Creator != App.logginedUser.Name) {
                    listView.Remove(contact);
                }
            }
            if (listView.Count() == 0) {
                NoProfiles.IsVisible = true;
            } else if (listView.Count() != 0 ) {
                NoProfiles.IsVisible = false;
            }
            contactList.ItemsSource = listView;
        }
    }
}