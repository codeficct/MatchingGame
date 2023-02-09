using MatchingGame.Clases;
using MatchingGame.Vistas.GamePage;
using MatchingGame.Vistas.LoginPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public static Action BackPressed;

        private bool AcceptBack;
        public GameSetting Setting { get; set; }
        int Score;
        public Home()
        {
            // Taks: connect database for data persistence
            Score = 0;
            Setting = new GameSetting
            {
                Score = 0
            };
            BindingContext = Setting;
            // From database
            // Application.Current.Properties["Score"] = 1;
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Score"))
            {
                lblScore.Text = Application.Current.Properties["Score"].ToString();
            }
        }

        private async void navigateToGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game(0, Score));
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        protected override bool OnBackButtonPressed()
        {
            if (AcceptBack)
                return false;

            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            if (await DisplayAlert("", "¿Estás seguro que quieres salir? 🥺", "Si", "No"))
            {
                AcceptBack = true;
                BackPressed();
            }
        }
    }
}