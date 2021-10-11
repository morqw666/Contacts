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

namespace FirstTry.View {    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage {
        public MainListView() {
            InitializeComponent();
        }
        public Command MenuItemDeleteCommand => new Command(OnDeleteCommand);
        private async void OnDeleteCommand(object contactObj) {
            Contact contact = new Contact();
            contact.Id = Convert.ToInt32(contactObj);
            if (contact == null) return;
            string option = await DisplayActionSheet("Confirm your actions", null, null, new string[] { "Delete Contact", "Cancel" });
            if (option == "Delete Contact") {
                App.DatabaseContact.DeleteItem(contact.Id);
            } else if (option == "Cancel") {
                return;
            }
            OnAppearing();
        }
        private async void OnClickAddProfile(object sender, EventArgs e) {
            Contact contact = new Contact();
            AddEditProfileView addEditProfileView = new AddEditProfileView();
            addEditProfileView.BindingContext = contact;
            await Navigation.PushAsync(addEditProfileView);
        }
        private void OnClickLogOut(object sender, EventArgs e) {
            SecureStorage.Remove("userKey");
            var page = new FirstPageView();
            NavigationPage.SetHasBackButton(page, false);
            Navigation.PushAsync(page);

        }
        protected override void OnAppearing() {
            base.OnAppearing();
            BindingContext = this;
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