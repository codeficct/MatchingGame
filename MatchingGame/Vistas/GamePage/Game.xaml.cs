using MatchingGame.Clases;
using MatchingGame.Vistas.HomePage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas.GamePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        int currentLevel = 0, rows = 3, columns = 2;
        int initialScore = 50, currentScore = 0;
        Matrix m1;
        Grid grid = new Grid
        {
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
        };
        public Game(int level, int Score)
        {
            currentLevel = level;

            currentScore = Score + initialScore + (level * 17);

            m1 = new Matrix();

            InitializeComponent();
            CardContainer.Content = grid;
            playGame.IsEnabled = true;
            playGame.Opacity = 1;
            rows += currentLevel; columns += currentLevel;
            m1.SetLevel(currentLevel, currentScore);
            generateNextLevel(true);
        }

        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }
        private async void playGame_Clicked(object sender, EventArgs e)
        {
            bool isContinue = true;
            int count = 0;
            playGame.IsEnabled = false;
            playGame.Opacity = 0.5;
            MatchImageRandom.Source = "defaultcard";
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 400), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    count++;
                    grid.Children.Clear();
                    StarGame();
                    if (count == 10)
                    {
                        isContinue = false;
                    }
                });
                return isContinue;
            });

            await Task.Delay(7000 + 7000 * (currentLevel / 5));
            m1.HideCards(ref grid);
            m1.SetMatchImage(ref MatchImageRandom);
        }

        public void generateNextLevel(bool isDefault = false)
        {
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            for (int r = 0; r < rows; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int c = 0; c < columns; c++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            m1.SetData(rows, columns, 1);
            if (isDefault)
                m1.GenerateDefaultCards(ref grid);
            else
                m1.GenerateCards(ref grid);
            if (CardContainer != null)
            {
                CardContainer.Content = grid;
            }
        }

        public void StarGame()
        {
            m1.Dispersion();
            m1.GenerateCards(ref grid);
        }
    }
}