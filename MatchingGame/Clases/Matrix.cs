using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MatchingGame.Clases
{
    internal class Matrix
    {
        private int rows, columns;
        private string match;
        private string[,] m;

        public Matrix()
        {
            initializeMatrix(0, 0);
        }

        public void initializeMatrix(int r, int c)
        {
            rows = r; columns = c;
            m = new string[r, c];
            match = "_1";
        }

        public void SetData(int r, int c, int min)
        {
            initializeMatrix(r, c);
            int countImg = min;
            for (int r1 = 0; r1 < rows; r1++)
                for (int c1 = 0; c1 < columns; c1++)
                {
                    m[r1, c1] = "_" + countImg;
                    countImg++;
                }
        }

        public void GenerateCards(ref Grid gridContent)
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                {
                    ImageButton imageButton = new ImageButton { Source = m[r, c], Aspect = Aspect.AspectFit, BackgroundColor = Color.FromHex("#7b5b9c"), BorderWidth = 2, BorderColor = Color.FromHex("#aaa"), CornerRadius = 16, ClassId = m[r, c], };
                    gridContent.Children.Add(imageButton, c, r);
                    imageButton.Clicked += async (sender, args) =>
                    {
                        this.IsMatch(imageButton.ClassId);
                    };
                }
        }

        async public void IsMatch(string id)
        {
            if (id == match)
            {
                await App.Current.MainPage.DisplayAlert("Is Matching!", id, "Ok");
            }
        }
    }
}
