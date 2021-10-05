using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstTry {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage {
        public FirstPage() {
            InitializeComponent();
        }
        //private void ButtonHello_Click(object sender, EventArgs e) {
        //    Label1.Text = loginEntry.Text;
        //}
        private async void ButtonSignUp_Click(object sender, EventArgs e) {
            await Navigation.PushAsync(new SignUpView());
        }

    }
}