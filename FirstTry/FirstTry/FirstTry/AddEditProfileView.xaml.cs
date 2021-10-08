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
    public partial class AddEditProfileView : ContentPage {
        //Image img;
        public AddEditProfileView() {
            InitializeComponent();        
            //img = new Image();

        }
        //private async void GetPhotoAsync(object sender, EventArgs e) {
        //    try {
        //        // выбираем фото
        //        var photo = await MediaPicker.PickPhotoAsync();
        //        // загружаем в ImageView
        //        img.Source = ImageSource.FromFile(photo.FullPath);
        //    } catch (Exception ex) {
        //        await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
        //    }
        //}
        private void OnClickSaveContact(object sender, EventArgs e) {
            var contact = (Contact)BindingContext;
            DateTime thisDay = DateTime.Now;
            contact.Date = thisDay.ToString();
            App.DatabaseContact.SaveItem(contact);
            Navigation.PopAsync();
        }
    }
}