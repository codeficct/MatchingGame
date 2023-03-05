using MatchingGame.Clases;
using MatchingGame.Vistas.HomePage;
using MatchingGame.Vistas.LoginPage;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;

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
                GetMyPositionLaderboard();
        }

        public async void GetMyPositionLaderboard()
        {
            try
            {
                ProfileContainer.Opacity = 0;
                ProfileContainer.IsVisible = false;
                Loading.IsRunning = true;
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
                            if (users[i].email == email)
                            {
                                Position.Text = (i + 1).ToString();

                                SelectAvatar.Source = users[i].photo;
                                Name.Text = users[i].name;
                                Email.Text = users[i].email;
                                Score.Text = users[i].score.ToString();
                                MaxLevel.Text = users[i].maxLevel.ToString();
                                ProfileContainer.Opacity = 1;
                                ProfileContainer.IsVisible = true;
                                Loading.IsRunning = false;
                                Loading.IsVisible = false;

                                Application.Current.Properties["Score"] = users[i].score;
                                Application.Current.Properties["MaxLevel"] = users[i].maxLevel;
                            }
                    }
                }
            }
            catch (Exception)
            {
                (Application.Current).MainPage = new NavigationPage(new Home());
            }
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("Token");
            Application.Current.Properties.Remove("Name");
            Application.Current.Properties.Remove("Email");
            Application.Current.Properties.Remove("Password");
            Application.Current.Properties["Score"] = 0;
            Application.Current.Properties.Remove("Photo");
            Application.Current.Properties.Remove("Id");
            Application.Current.Properties["MaxLevel"] = 0;
            await Application.Current.SavePropertiesAsync();
            (Application.Current).MainPage = new NavigationPage(new Home());

        }

        protected override bool OnBackButtonPressed()
        {
            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            await Navigation.PopModalAsync();
        }
    }
}