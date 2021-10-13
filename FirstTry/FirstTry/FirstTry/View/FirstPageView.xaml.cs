using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstTry.ViewModel;

namespace FirstTry.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPageView : ContentPage {
        public FirstPageView() {
            InitializeComponent();
            BindingContext = new FirstPageViewModel(Navigation);
        }
        public void GetLogin(string getLogin) {
            loginEntry.Text = getLogin;
        }
    }
}