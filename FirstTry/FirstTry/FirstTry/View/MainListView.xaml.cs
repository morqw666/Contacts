using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstTry.ViewModel;

namespace FirstTry.View {  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage {
        public MainListView() {
            InitializeComponent();
            BindingContext = new MainListViewModel(Navigation);
        }
    }
}