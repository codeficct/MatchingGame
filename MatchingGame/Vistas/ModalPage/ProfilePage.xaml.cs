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

namespace MatchingGame.Vistas.ModalPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Photo")
                && Application.Current.Properties.ContainsKey("Name")
                && Application.Current.Properties.ContainsKey("Email")
                && Application.Current.Properties.ContainsKey("Score"))
            {
                SelectAvatar.Source = Application.Current.Properties["Photo"] as string;
                Name.Text = Application.Current.Properties["Name"] as string;
                Email.Text = Application.Current.Properties["Email"] as string;
                Score.Text = Application.Current.Properties["Score"].ToString();
                MaxLevel.Text = Application.Current.Properties["MaxLevel"].ToString();
                GetMyPositionLaderboard();
            }
        }

        public async void GetMyPositionLaderboard()
        {
            try
            {
                Uri RequestUri = new("https://matchinggame.vercel.app/api/matching-game");
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(RequestUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    User[] users = JsonConvert.DeserializeObject<User[]>(content);

                    if (Application.Current.Properties.ContainsKey("Email"))
                    {
                        var email = Application.Current.Properties["Email"] as string;
                        for (int i = 0; i < users.Length; i++)
                        {
                            if (users[i].email == email)
                            {
                                Position.Text = (i + 1).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Error", error.Message, "Cancelar");
            }
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("Token");
            Application.Current.Properties.Remove("Name");
            Application.Current.Properties.Remove("Email");
            Application.Current.Properties.Remove("Password");
            Application.Current.Properties.Remove("Score");
            Application.Current.Properties.Remove("Photo");
            Application.Current.Properties.Remove("Id");
            Application.Current.Properties.Remove("MaxLevel");

            await Application.Current.SavePropertiesAsync();
            await Navigation.PushAsync(new Home());
        }

        protected override bool OnBackButtonPressed()
        {
            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            await Navigation.PopToRootAsync();
        }
    }
}