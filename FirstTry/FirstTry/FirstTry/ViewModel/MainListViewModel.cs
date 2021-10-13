using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using FirstTry.View;
using Xamarin.Essentials;
using Acr.UserDialogs;
using System.Linq;

namespace FirstTry.ViewModel {
    public class MainListViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand MenuItemDeleteCommand { get; private set; }
        public ICommand MenuItemEditCommand { get; private set; }
        public ICommand ButtoneAddProfile { get; private set; }
        public ICommand LogOut { get; private set; }
        public ICommand Refresh { get; private set; }
        public readonly INavigation Navigation;
        public MainListViewModel(INavigation navigation) {
            MenuItemDeleteCommand = new Command(OnDeleteCommand);
            MenuItemEditCommand = new Command(OnEditCommand);
            ButtoneAddProfile = new Command(OnAddProfile);
            LogOut = new Command(OnLogOut);
            Refresh = new Command(OnRefresh); 
            Navigation = navigation;
            OnRefresh();
        }
        public bool IsVisible => Contacts.Count() == 0;
        private void OnRefresh() {
            GetContactList();
        }
        private void GetContactList() {
            var listView = App.DatabaseContact.GetItems().ToList();
            for (int i = listView.Count() - 1; i >= 0; i--) {
                var contact = listView.ElementAt(i);
                if (contact.Creator != App.logginedUser.Name) {
                    listView.Remove(contact);
                }
            }
            Contacts = listView;
        }
        private List<Contact> _contacts = new List<Contact>();
        public List<Contact> Contacts {
            get { return _contacts; }
            set {
                if (_contacts != value) {
                    _contacts = value;
                    OnPropertyChanged("Contacts");
                    OnPropertyChanged("IsVisible");
                }
            }
        }  
        private async void OnDeleteCommand(object contactObj) {
            Contact contact = new Contact();
            contact.Id = Convert.ToInt32(contactObj);
            if (contact == null) return;
            var option = await UserDialogs.Instance.ConfirmAsync("Would you like to remove contact?", "Confirm Selection", "Yes", "No");
            if (option == true) {
                App.DatabaseContact.DeleteItem(contact.Id);
                OnRefresh();
            } else if (option == false) {
                return;
            }
        }
        private async void OnEditCommand(object contactObj) {
            Contact contact = new Contact();
            int contactID = Convert.ToInt32(contactObj);
            contact = App.DatabaseContact.GetItem(contactID);
            var page = new AddEditProfileView(contact);
            await Navigation.PushAsync(page);
        }
        private void OnAddProfile() {
            Navigation.PushAsync(new AddEditProfileView());
        }
        private void OnLogOut() {
            SecureStorage.Remove("userKey");
            var page = new FirstPageView();
            NavigationPage.SetHasBackButton(page, false);
            Navigation.PushAsync(page);
        }
        protected void OnPropertyChanged(string propName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
