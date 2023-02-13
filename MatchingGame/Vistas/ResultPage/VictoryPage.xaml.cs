using MatchingGame.Clases;
using MatchingGame.Vistas.GamePage;
using MatchingGame.Vistas.HomePage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            if (Application.Current.Properties.ContainsKey("Score") || Application.Current.Properties.ContainsKey("MaxLevel"))
            {
                Application.Current.Properties["Score"] = (int)Application.Current.Properties["Score"] + currentScore;
                if ((int)Application.Current.Properties["MaxLevel"] < level)
                    Application.Current.Properties["MaxLevel"] = level;
                
                CurrentScore.Text = Application.Current.Properties["Score"].ToString();
                isFirstRender = false;
                if (Application.Current.Properties.ContainsKey("Id") && Application.Current.Properties.ContainsKey("Token"))
                    UpdateScore(currentScore, (int)Application.Current.Properties["MaxLevel"], Application.Current.Properties["Id"].ToString());
            }

            if (isFirstRender)
            {
                CurrentScore.Text = Score.ToString();
                Application.Current.Properties["Score"] = Score;
            }
        }

        public async void UpdateScore(int Score, int level, string id)
        {
            Score objScore = new() { score = Score, maxLevel = level };
            Uri RequestUri = new($"https://matchinggame.vercel.app/api/matching-game/{id}");
            var client = new HttpClient();
            var jsonScore = JsonConvert.SerializeObject(objScore);
            var contentJsonScore = new StringContent(jsonScore, Encoding.UTF8, "application/json");

            await client.PutAsync(RequestUri, contentJsonScore);
        }

        private async void NextLevel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game(currentLevel, currentScore));
        }

        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return true;
        }
    }
}