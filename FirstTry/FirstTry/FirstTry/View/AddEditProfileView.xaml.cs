using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstTry.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage {
        Image img;
        string photoPath;
        public AddEditProfileView() {
            InitializeComponent();
            img = new Image();
        }
        //ICommand CreateActionSheetCommand(bool useBottomSheet, bool cancel, int items, string message = null) {
        //    return new Command(() => {
        //        var cfg = new ActionSheetConfig()
        //            .SetTitle("**** my Test Title ***")
        //            .SetMessage(message)
        //            .Add("Default", null, "emoji_cool_small.png")
        //            .Add("E-Mail", null, "emoji_cool_small.png")
        //            .SetUseBottomSheet(useBottomSheet);
        //    }
        //}
        public void GetContact(Contact contact) {
            if (contact.Image == "avatar.png") {
                imgBtn.Source = "avatarAdd.png";
            } else {
                imgBtn.Source = contact.Image.ToString();
            }
            DisplayAlert("INFO", imgBtn.Source.ToString(), "OK");
        }
        private async void BtnActionSheet_Clicked(object sender, System.EventArgs e) {
            string option;
            if (imgBtn.Source.ToString() == "File: avatarAdd.png") {
                option = await DisplayActionSheet("Choose option", "Cancel", null, new string[] { "Get Photo", "Take Photo"});
            } else {
                option = await DisplayActionSheet("Choose option", "Cancel", null, new string[] { "Get Photo", "Take Photo", "Remove Photo" });
            }
            //string option = await DisplayActionSheet("Choose option", "Cancel", null, new string[] { "Get Photo", "Take Photo", "Remove Photo" });
            if (option == "Get Photo") {
                GetPhotoAsync(sender, e);
            } else if (option == "Take Photo") {
                TakePhotoAsync(sender, e);
            } else if (option == "Remove Photo") {
                imgBtn.Source = "avatarAdd.png";
            }
        }
        private async void GetPhotoAsync(object sender, EventArgs e) {
            try {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
                imgBtn.Source = img.Source;
                photoPath = photo.FullPath;
            } catch (Exception ex) {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private async void TakePhotoAsync(object sender, EventArgs e) {
            try {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                //сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
                imgBtn.Source = img.Source;
                photoPath = photo.FullPath;
            } catch (Exception ex) {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
        private void OnClickSaveContact(object sender, EventArgs e) {
            var contact = (Contact)BindingContext;
            DateTime thisDay = DateTime.Now;
            contact.Date = thisDay.ToString();
            if (photoPath == null) {
                contact.Image = "avatar.png";
            } else {
                contact.Image = photoPath;
            }
            contact.Creator = App.logginedUser.Name;
            App.database.SaveItem(App.logginedUser);
            App.DatabaseContact.SaveItem(contact);
            Navigation.PopAsync();
        }
    }
}