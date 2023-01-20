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
        int currentLevel;
        public VictoryPage(int level)
        {
            currentLevel = level;
            InitializeComponent();
        }

        private async void NextLevel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game(currentLevel));
        }

        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }
    }
}