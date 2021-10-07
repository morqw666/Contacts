using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstTry {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage {
        public AddEditProfileView() {
            InitializeComponent();
            Contact contact = new Contact();
            BindingContext = contact;
        }
        private void OnClickSaveContact(object sender, EventArgs e) {
            var contact = (Contact)BindingContext;
            DateTime thisDay = DateTime.Now;
            contact.Date = thisDay.ToString();
            App.DatabaseContact.SaveItem(contact);
            Navigation.PopAsync();
        }
    }
}