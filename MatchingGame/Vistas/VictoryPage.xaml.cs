using MatchingGame.Clases;
using MatchingGame.Vistas.GamePage;
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
    public partial class VictoryPage : ContentPage
    {
        int currentLevel, currentScore;
        public VictoryPage(int level, int Score)
        {
            bool isFirstRender = true;
            currentLevel = level;
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            PointsEarned.Text = Score.ToString();
            currentScore = Score;
            // Application.Current.Properties["Score"] = Score;
            if (Application.Current.Properties.ContainsKey("Score"))
            {
                Application.Current.Properties["Score"] = (int)Application.Current.Properties["Score"] + currentScore;
                CurrentScore.Text = Application.Current.Properties["Score"].ToString();
                isFirstRender = false;
            }

            if (isFirstRender)
            {
                CurrentScore.Text = Score.ToString();
                Application.Current.Properties["Score"] = Score;
            }
        }

        private async void NextLevel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game(currentLevel, currentScore));
        }

        private async void CloseBtn_Clicked(object sender, EventArgs e)
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