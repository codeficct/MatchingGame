using MatchingGame.Clases;
using MatchingGame.Vistas.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas.LoginPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvatarsPage : ContentPage
    {
        Grid grid = new Grid
        {
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
        };
        string Name, Email, Password, ConfirmPassword, imageSelected;
        int rows = 8, columns = 2;
        public string[,] avatarsUrl = new string[,]
        {
            { "https://i.ibb.co/HF8xr9L/1.png", "https://i.ibb.co/p4g3kRT/2.png" },
            { "https://i.ibb.co/6t15gLB/3.png", "https://i.ibb.co/TqMsjx7/4.png" },
            { "https://i.ibb.co/DRd03qy/5.png", "https://i.ibb.co/NFrG2hm/6.png" },
            { "https://i.ibb.co/ZYgR13F/7.png", "https://i.ibb.co/YbSSK25/8.png" },
            { "https://i.ibb.co/nDdT1CX/9.png", "https://i.ibb.co/v3wDLZJ/10.png" },
            { "https://i.ibb.co/vDM18BM/11.png", "https://i.ibb.co/KrNPw32/12.png" },
            { "https://i.ibb.co/k3YmLRF/13.png", "https://i.ibb.co/XjyQcKz/14.png" },
            { "https://i.ibb.co/4Fkmr9D/15.png", "https://i.ibb.co/vHwhkyg/16.png " }
        };

        public AvatarsPage(string name, string email, string password, string confirmPassword, string avatar = "https://i.ibb.co/DRd03qy/5.png")
        {
            Name = name; Email = email; Password = password; ConfirmPassword = confirmPassword;
            imageSelected = avatar;
            InitializeComponent();
            BindingContext = this;
            AvatarsContainer.Content = grid;
            GenerateAvatars(ref grid);
            DeselectAvatars();
        }

        public void GenerateAvatars(ref Grid grid)
        {
            for (int r = 0; r < rows; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int c = 0; c < columns; c++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                {
                    ImageButton imageButton = new ImageButton { Source = avatarsUrl[r, c], Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent, BorderWidth = 6, BorderColor = Color.Transparent, CornerRadius = 1000, ClassId = avatarsUrl[r, c], HeightRequest = 140, WidthRequest = 140, Margin = 16, HorizontalOptions = LayoutOptions.Center };
                    grid.Children.Add(imageButton, c, r);
                    imageButton.Clicked += (sender, args) =>
                    {
                        imageSelected = ((ImageButton)sender).ClassId;
                        DeselectAvatars();
                    };
                }
        }

        private async void SelectAvatar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(Name, Email, Password, ConfirmPassword,imageSelected));
        }

        public void DeselectAvatars()
        {
            foreach (ImageButton item in grid.Children)
            {
                if (imageSelected == item.ClassId)
                    item.BorderColor = Color.FromHex("#fe207d");
                else
                    item.BorderColor = Color.Transparent;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            await Navigation.PushAsync(new RegisterPage(Name, Email, Password, ConfirmPassword, imageSelected));
        }
    }
}