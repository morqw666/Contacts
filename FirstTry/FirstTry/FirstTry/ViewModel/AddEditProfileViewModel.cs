using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using FirstTry.View;
using Xamarin.Essentials;
using Acr.UserDialogs;
using System.IO;

namespace FirstTry.ViewModel {
    public class AddEditProfileViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        Image img;
        string photoPath;
        public ICommand SaveContact { get; private set; }
        public ICommand ImgActionSheet { get; private set; }
        public Contact contact { get; private set; }
        public readonly INavigation Navigation;
        public AddEditProfileViewModel(INavigation navigation, Contact con) {
            SaveContact = new Command(OnSaveContact);
            ImgActionSheet = new Command(OnImgActionSheet);
            img = new Image();
            contact = con;
            Navigation = navigation;
            GetImage();
        }
        public string NickName {
            get { return contact.NickName; }
            set {
                if (contact.NickName != value) {
                    contact.NickName = value;
                    OnPropertyChanged("NickName");
                }
            }
        }
        public string FullName {
            get { return contact.FullName; }
            set {
                if (contact.FullName != value) {
                    contact.FullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }
        public string Description {
            get { return contact.Description; }
            set {
                if (contact.Description != value) {
                    contact.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public string Image {
            get { return contact.Image; }
            set {
                if (contact.Image != value) {
                    contact.Image = value;
                    OnPropertyChanged("Image");
                }
            }
        }
        public void GetImage() {
            if (contact.Image == "avatar.png" || contact.Image == null) {
                Image = "avatarAdd.png";
            } else {
                Image = contact.Image.ToString();
            }
        }
        private void OnImgActionSheet() {
            if (Image == "avatarAdd.png") {
                UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                        .SetTitle("Choose an action")
                        .Add("Get photo", GetPhotoAsync, "gallery.png")
                        .Add("Take photo", TakePhotoAsync, "camera.png")
                    );
            } else {
                UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                        .SetTitle("Choose an action")
                        .Add("Get photo", GetPhotoAsync, "gallery.png")
                        .Add("Take photo", TakePhotoAsync, "camera.png")
                        .Add("Remove photo", RemovePhoto, "recycle.png")
                    );
            }
        }
        private async void GetPhotoAsync() {
            try {
                var photo = await MediaPicker.PickPhotoAsync();
                img.Source = ImageSource.FromFile(photo.FullPath);
                Image = photo.FullPath;
                photoPath = photo.FullPath;
            } catch (Exception ex) {
                UserDialogs.Instance.Alert(new AlertConfig() {
                    Title = "Error",
                    Message = ex.Message,
                });
            }
        }
        private async void TakePhotoAsync() {
            try {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                img.Source = ImageSource.FromFile(photo.FullPath);
                Image = photo.FullPath;
                photoPath = photo.FullPath;
            } catch (Exception ex) {
                UserDialogs.Instance.Alert(new AlertConfig() {
                    Title = "Error",
                    Message = ex.Message,
                });
            }
        }
        private void RemovePhoto() {
            Image = "avatarAdd.png";
        }
        private void OnSaveContact() {
            DateTime thisDay = DateTime.Now;
            contact.Date = thisDay.ToString();
            if (Image == "avatarAdd.png") {
                contact.Image = "avatar.png";
            } else if (photoPath != null) {
                contact.Image = photoPath;
            } else {
                Image = contact.Image;
            }
            contact.Creator = App.logginedUser.Name;
            App.database.SaveItem(App.logginedUser);
            App.DatabaseContact.SaveItem(contact);
            var page = new MainListView();
            NavigationPage.SetHasBackButton(page, false);
            Navigation.PushAsync(page);
        }
        protected void OnPropertyChanged(string propName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
