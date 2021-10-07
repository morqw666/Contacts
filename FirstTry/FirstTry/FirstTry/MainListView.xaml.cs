using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstTry {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage {
        public MainListView() {
            InitializeComponent();
            var listView = App.DatabaseContact.GetItems();
            contactList.ItemsSource = listView;
        }
        //private void SaveContact(object sender, EventArgs e) {
        //    var contact = (Contact)BindingContext;
        //    App.DatabaseContact.SaveItem(contact);
        //}
        private void OnClickLogOut(object sender, EventArgs e) {
            SecureStorage.Remove("userKey");
            var page = new FirstPage();
            NavigationPage.SetHasBackButton(page, false);
            Navigation.PushAsync(page);

        }
    }
}