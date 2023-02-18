using MatchingGame.Clases;
using MatchingGame.Vistas.GamePage;
using MatchingGame.Vistas.LoginPage;
using MatchingGame.Vistas.ModalPage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MatchingGame.Vistas.HomePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public static Action BackPressed;
        string AppVersion = "0.1.1";

        public GameSetting Setting { get; set; }
        int Score;
        public Home()
        {
            Score = 0;
            Setting = new GameSetting
            {
                Score = 0
            };
            BindingContext = Setting;
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Token")
                && Application.Current.Properties.ContainsKey("Name"))
            {
                GetUserLoggedIn(Application.Current.Properties["Id"].ToString());
                UserName.Text = Application.Current.Properties["Name"].ToString();
                Register.Text = "Ver Perfil";
            }
            else
            {
                UserName.Text = "Ficct-uagrm";
                Register.Text = "Iniciar Sesión";
            }
            if (Application.Current.Properties.ContainsKey("Score"))
                lblScore.Text = Application.Current.Properties["Score"].ToString();

            GetVersionApp();
        }

        public async void GetUserLoggedIn(string id)
        {
            try
            {
                Uri RequestUri = new($"https://matchinggame.vercel.app/api/matching-game/{id}");
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(RequestUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    User user = JsonConvert.DeserializeObject<User>(content);
                    Application.Current.Properties["Name"] = user.name;
                    Application.Current.Properties["Photo"] = user.photo;
                    Application.Current.Properties["Email"] = user.email;
                    Application.Current.Properties["Score"] = user.score;
                    lblScore.Text = user.score.ToString();
                    Application.Current.Properties["MaxLevel"] = user.maxLevel;
                }
            }
            catch (Exception)
            {
                (Application.Current).MainPage = new NavigationPage(new Home());
            }
        }

        public async void GetVersionApp()
        {
            try
            {
                // Uri RequestUri = new Uri("https://api.github.com/repos/codeficct/MatchingGame/releases/latest");
                Uri RequestUri = new("https://matchinggame.vercel.app/api/matching-game/version");
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(RequestUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Clases.Version[] version = JsonConvert.DeserializeObject<Clases.Version[]>(content);
                    if (version[0].AppVersion != AppVersion)
                    {
                        string lastAndCurrent = $"De v{AppVersion} a ✨v{version[0].AppVersion} \n\n";
                        string developer = $"Desarrollador: {version[0].Developer} \n\n";
                        string Copyright = $"Copyright © {DateTime.Now.Year} Matching Game";
                        if (await DisplayAlert(version[0].Title,
                            $"{version[0].Description} \n\n" + lastAndCurrent + developer + Copyright
                            , "Actualizar", "Cancelar"))
                            await Launcher.OpenAsync("https://matchinggame.vercel.app");
                    }
                }
            }
            catch (Exception)
            {
                (Application.Current).MainPage = new NavigationPage(new Home());
            }
        }

        private async void navigateToGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game(0, Score));
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("Token")
                && Application.Current.Properties.ContainsKey("Name"))
                await Navigation.PushModalAsync(new ProfilePage());
            else
                await Navigation.PushAsync(new RegisterPage());
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
                if (existingPages[existingPages.Count - 1] != page)
                    Navigation.RemovePage(page);

            return false;
        }

        private async void NavigateToInfoPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new InfoPage());
        }
    }
}