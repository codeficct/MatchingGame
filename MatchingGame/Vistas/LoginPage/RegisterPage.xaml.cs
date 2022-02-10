using MatchingGame.Vistas.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MatchingGame.Clases;
using System.Net.Http;
using System.Net;
using System.Runtime.CompilerServices;

namespace MatchingGame.Vistas.LoginPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        string currentAvatar;
        public RegisterPage(string name = "", string email = "", string password = "", string confirmPassword = "", string avatar = "addProfile")
        {
            currentAvatar = avatar;
            InitializeComponent();
            BoxName.Text = name;
            BoxEmail.Text = email;
            BoxPassword.Text = password;
            BoxPasswordRepeat.Text = confirmPassword;
            SelectAvatar.Source = avatar;
        }

        private async void LogIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        protected override bool OnBackButtonPressed()
        {
            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            await Navigation.PushAsync(new Home());
        }

        private async void SelectAvatar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AvatarsPage(BoxName.Text, BoxEmail.Text, BoxPassword.Text, BoxPasswordRepeat.Text, currentAvatar));
        }

        private void BoxPasswordRepeat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BoxPassword.Text != BoxPasswordRepeat.Text)
                BoxPasswordRepeat.TextColor = Color.FromHex("#D2001A");
            else
                BoxPasswordRepeat.TextColor = Color.FromHex("#fff");
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            try
            {
                Loading.IsRunning = true;
                lblError.Opacity = 0;
                if (currentAvatar == "addProfile" || BoxName.Text == "" || BoxEmail.Text == "" || BoxPassword.Text == "" || BoxPasswordRepeat.Text == "")
                    throw new ArgumentException("Por favor, rellene todos los campos.");

                Register newUser = new()
                {
                    photo = currentAvatar,
                    name = BoxName.Text,
                    email = BoxEmail.Text,
                    password = BoxPassword.Text,
                    confirmPassword = BoxPasswordRepeat.Text
                };

                Uri RequestUri = new("https://matchinggame.vercel.app/api/auth/register");
                var client = new HttpClient();
                var jsonRegister = JsonConvert.SerializeObject(newUser);
                var contentJsonRegister = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJsonRegister);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    Login login = new()
                    {
                        email = newUser.email,
                        password = newUser.password
                    };
                    Uri RequestLogin = new("https://matchinggame.vercel.app/api/auth/login");
                    var jsonLogin = JsonConvert.SerializeObject(login);
                    var contentJsonLogin = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
                    var responseLogin = await client.PostAsync(RequestLogin, contentJsonLogin);
                    if (responseLogin.StatusCode == HttpStatusCode.OK)
                    {
                        var resultJson = await responseLogin.Content.ReadAsStringAsync();
                        var results = JsonConvert.DeserializeObject<User>(resultJson);

                        Application.Current.Properties["Name"] = results.name;
                        Application.Current.Properties["Photo"] = results.photo;
                        Application.Current.Properties["Email"] = results.email;
                        if (Application.Current.Properties.ContainsKey("Score"))
                            Application.Current.Properties["Score"] =
                                results.score + (int)Application.Current.Properties["Score"];
                        else
                            Application.Current.Properties["Score"] = results.score;
                        Application.Current.Properties["Id"] = results._id;
                        Application.Current.Properties["Password"] = results.password;
                        Application.Current.Properties["Token"] = results.token;
                        //Application.Current.Properties["MaxLevel"] = results.maxlevel;

                        await Navigation.PushAsync(new Home());
                    }
                } else
                {
                    throw new ArgumentException("Intentar otra vez.");
                }
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