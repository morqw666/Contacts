using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstTry.ViewModel;

namespace FirstTry.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage {
        public SignUpView() {
            InitializeComponent();
            BindingContext = new SignUpViewModel(Navigation);
        } 
    }
}
