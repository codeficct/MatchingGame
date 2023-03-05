using MatchingGame.Clases;
using MatchingGame.Vistas.HomePage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            if (Application.Current.Properties.ContainsKey("Score") && Application.Current.Properties.ContainsKey("MaxLevel"))
            {
                Application.Current.Properties["Score"] = (int)Application.Current.Properties["Score"] + Score;
                CurrentScore.Text = Application.Current.Properties["Score"].ToString();
                isFirstRender = false;
                if (Application.Current.Properties.ContainsKey("Id") && Application.Current.Properties.ContainsKey("Token"))
                    UpdateScore(Score, (int)Application.Current.Properties["MaxLevel"], Application.Current.Properties["Id"].ToString());
            }

            if (isFirstRender)
            {
                Application.Current.Properties["Score"] = Score;
                Application.Current.Properties["MaxLevel"] = Score;
            }
        }

        public async void UpdateScore(int Score, int level, string id)
        {
            try
            {
                Score objScore = new() { score = Score, maxLevel = level };
                Uri RequestUri = new($"https://matchinggame.vercel.app/api/matching-game/{id}");
                var client = new HttpClient();
                var jsonScore = JsonConvert.SerializeObject(objScore);
                var contentJsonScore = new StringContent(jsonScore, Encoding.UTF8, "application/json");

                await client.PutAsync(RequestUri, contentJsonScore);
            }
            catch (Exception)
            {
                (Application.Current).MainPage = new NavigationPage(new Home());
            }
        }

        private async void CloseOverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}