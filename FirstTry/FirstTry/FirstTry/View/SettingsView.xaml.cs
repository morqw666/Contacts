using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstTry.ViewModel;

namespace FirstTry.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage {
        public SettingsView() {
            InitializeComponent();
            BindingContext = new SettingsViewModel(Navigation);
        }
    }
}