using MatchingGame.Vistas.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverPage : ContentPage
    {
        public GameOverPage(int Score)
        {
            bool isFirstRender = true;
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            Symbol.Text = Score < 0 ? "-" : "+";
            Symbol.TextColor = Score < 0 ? Color.FromHex("#D2001A") : Color.FromHex("#03C988");

            PointsEarned.Text = Math.Abs(Score).ToString();
            PointsEarned.TextColor = Score < 0 ? Color.FromHex("#D2001A") : Color.FromHex("#03C988");

            //CurrentScore.Text = (Score).ToString();

            if (Application.Current.Properties.ContainsKey("Score"))
            {
                Application.Current.Properties["Score"] = (int)Application.Current.Properties["Score"] + Score;
                CurrentScore.Text = Application.Current.Properties["Score"].ToString();
                isFirstRender = false;
            }

            if (isFirstRender)
            {
                Application.Current.Properties["Score"] = Score;
            }
        }

        private async void CloseOverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return true;
        }
    }
}