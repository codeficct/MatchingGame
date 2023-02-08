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
            InitializeComponent();
            Symbol.Text = Score < 0 ? "-" : "+";
            Symbol.TextColor = Score < 0 ? Color.FromHex("#D2001A") : Color.FromHex("#03C988");

            PointsEarned.Text = Math.Abs(Score).ToString();
            PointsEarned.TextColor = Score < 0 ? Color.FromHex("#D2001A") : Color.FromHex("#03C988");

            CurrentScore.Text = (Score).ToString();
        }

        private async void CloseOverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }
    }
}