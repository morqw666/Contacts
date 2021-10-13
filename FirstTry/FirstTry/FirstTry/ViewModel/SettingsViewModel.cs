using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FirstTry.ViewModel {
    public class SettingsViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public readonly INavigation Navigation;
        public SettingsViewModel(INavigation navigation) {
            Navigation = navigation;
        }
            protected void OnPropertyChanged(string propName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
