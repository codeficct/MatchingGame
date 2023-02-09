using MatchingGame.Vistas.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas.LoginPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        protected override bool OnBackButtonPressed()
        {
            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            await Navigation.PushAsync(new Home());
        }
    }
}