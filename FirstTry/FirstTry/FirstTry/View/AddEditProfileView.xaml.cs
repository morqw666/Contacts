using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstTry.ViewModel;

namespace FirstTry.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage {
        public AddEditProfileView() {
            InitializeComponent();
            BindingContext = new AddEditProfileViewModel(Navigation, new Contact());
        }
        public AddEditProfileView(Contact contact) {
            InitializeComponent();
            BindingContext = new AddEditProfileViewModel(Navigation, contact);
        }
    }
}