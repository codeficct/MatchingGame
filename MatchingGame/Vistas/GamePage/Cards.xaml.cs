using MatchingGame.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
//new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
namespace MatchingGame.Vistas.GamePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cards : ContentView
    {
        //Matrix m1;
        //Grid grid1 = new Grid
        //{
        //    VerticalOptions = LayoutOptions.FillAndExpand,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //}; 
        
        public Cards()
        {
            //grid1.RowDefinitions.Add(new RowDefinition());
            //grid1.RowDefinitions.Add(new RowDefinition());
            //grid1.RowDefinitions.Add(new RowDefinition());

            //grid1.ColumnDefinitions.Add(new ColumnDefinition());
            //grid1.ColumnDefinitions.Add(new ColumnDefinition());

            //m1 = new Matrix();
            //m1.SetData(3, 2, 1);
            //m1.GenerateCards(ref grid1);

            //Content = grid1;
            InitializeComponent();
        }
    }
}