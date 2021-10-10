using System;
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
        public MainListView() {
            InitializeComponent();
            var listView = App.DatabaseContact.GetItems();
            contactList.ItemsSource = listView;
        }
        public ICommand MenuItemDeleteCommand => new Command(MenuItemDelete);
        private void MenuItemDelete(object contactObj) {
            Contact contact = contactObj as Contact;
            if (contact == null) return;
            App.DatabaseContact.DeleteItem(contact.Id);
            //var contactobj = (CategoryItem)BindingContext;
            //await App.DatabaseContact.DeleteItem(contact);
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
    }
}