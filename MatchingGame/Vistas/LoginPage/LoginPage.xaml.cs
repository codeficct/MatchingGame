using MatchingGame.Clases;
using MatchingGame.Vistas.HomePage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas.LoginPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
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

        private async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                Loading.IsRunning = true;
                lblError.Opacity = 0;
                if (BoxEmail.Text == "" || BoxPassword.Text == "")
                    throw new ArgumentException("Por favor, rellene todos los campos.");
                Login user = new()
                {
                    email = BoxEmail.Text,
                    password = BoxPassword.Text
                };

                Uri RequestUri = new("https://matchinggame.vercel.app/api/auth/login");
                var client = new HttpClient();
                var jsonUser = JsonConvert.SerializeObject(user);
                var contentJsonLogin = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJsonLogin);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<User>(resultJson);

                    Application.Current.Properties["Name"] = results.name;
                    Application.Current.Properties["Photo"] = results.photo;
                    Application.Current.Properties["Email"] = results.email;
                    Application.Current.Properties["Score"] = results.score;
                    Application.Current.Properties["Id"] = results._id;
                    Application.Current.Properties["Password"] = results.password;
                    Application.Current.Properties["Token"] = results.token;
                    Application.Current.Properties["MaxLevel"] = results.maxLevel;
                    (Application.Current).MainPage = new NavigationPage(new Home());
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ErrorAuth errorMessage = JsonConvert.DeserializeObject<ErrorAuth>(await response.Content.ReadAsStringAsync());
                    throw new ArgumentException($"{errorMessage.message}");
                }
                else
                    throw new ArgumentException("Intentar otra vez.");
            }
            catch (Exception error)
            {
                Loading.IsRunning = false;
                lblError.Text = error.Message;
                lblError.Opacity = 1;
            }
        }
    }
}