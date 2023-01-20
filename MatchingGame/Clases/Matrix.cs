using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using MatchingGame.Vistas.GamePage;
using MatchingGame.Vistas;

namespace MatchingGame.Clases
{
    public class Matrix
    {
        private int rows, columns;
        public string match;
        public int Level;
        private string[,] m;

        public Matrix()
        {
            initializeMatrix(0, 0);
        }

        public void initializeMatrix(int r, int c)
        {
            rows = r; columns = c;
            m = new string[r, c];
            match = "defaultcard";
        }

        public void SetLevel(int level)
        {
            Level = level;
        }

        public void SetMatchImage(ref ImageButton MatchImageRandom)
        {
            Random numRnd = new Random();
            match = m[numRnd.Next(0, rows - 1), numRnd.Next(0, columns - 1)];
            MatchImageRandom.Source = match;
        }

        public void SetData(int r, int c, int min)
        {
            Random numRandom = new Random();
            initializeMatrix(r, c);
            int countImg = min;
            for (int r1 = 0; r1 < rows; r1++)
                for (int c1 = 0; c1 < columns; c1++)
                {
                    m[r1, c1] = "_" + numRandom.Next(min, 34);
                    countImg++;
                }
        }

        public void GenerateDefaultCards(ref Grid grid)
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                {
                    // BoxView boxview = new BoxView { BackgroundColor = Color.FromHex("#7b5b9c"), CornerRadius = 16, HeightRequest =  };
                    ImageButton imageButton = new ImageButton { Source = "question", Padding = new Thickness(0, 30), Aspect = Aspect.AspectFit, BackgroundColor = Color.FromHex("#7b5b9c"), BorderWidth = 2, BorderColor = Color.FromHex("#aaa"), CornerRadius = 16 };

                    grid.Children.Add(imageButton, c, r);
                }
        }

        public void GenerateCards(ref Grid gridContent)
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                {
                    ImageButton imageButton = new ImageButton { Source = m[r, c], Aspect = Aspect.AspectFit, BackgroundColor = Color.FromHex("#7b5b9c"), BorderWidth = 2, BorderColor = Color.FromHex("#aaa"), CornerRadius = 16, ClassId = m[r, c] };
                    gridContent.Children.Add(imageButton, c, r);
                    imageButton.Clicked += async (sender, args) =>
                    {
                        // ((ImageButton)sender).Source = ((ImageButton)sender).ClassId;
                        imageButton.Source = ((ImageButton)sender).ClassId;
                        this.IsMatch(imageButton.ClassId);
                        await Task.Delay(1000);
                        ((ImageButton)sender).Source = "question";
                    };
                }
        }

        public async void IsMatch(string id)
        {
            if (id == match)
            {
                await Task.Delay(500);
                // await App.Current.MainPage.DisplayAlert("Is Matching!", id, "Ok");
                await App.Current.MainPage.Navigation.PushAsync(new VictoryPage(Level + 1));
            }
        }

        public void SwapItems(int r1, int c1, int r2, int c2)
        {
            string aux = m[r1, c1];
            m[r1, c1] = m[r2, c2];
            m[r2, c2] = aux;
        }

        public void Dispersion()
        {
            Random numRandom = new Random();
            int r1, c1, r2, c2;
            for (int r = 0; r < Math.Sqrt(rows); r++)
            {
                for (int c = 0; c < Math.Sqrt(columns); c++)
                {
                    r1 = numRandom.Next(0, rows);
                    c1 = numRandom.Next(0, columns);
                    r2 = numRandom.Next(0, rows);
                    c2 = numRandom.Next(0, columns);
                    SwapItems(r1, c1, r2, c2);
                }
            }
        }

        public void HideCards(ref Grid grid)
        {
            foreach (ImageButton item in grid.Children)
            {
                item.Source = "question";
            }
        }
    }
}
