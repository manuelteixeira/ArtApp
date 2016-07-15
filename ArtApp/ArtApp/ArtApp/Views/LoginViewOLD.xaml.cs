using System;
using System.Net.Http;
using Xamarin.Forms;

namespace ArtApp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            enterButton.Clicked += enterButton_Clicked;
           

        }

        private async void enterButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userEntry.Text) || string.IsNullOrEmpty(userPassword.Text))
            {
                await DisplayAlert("Error", "Please insert a username and password", "Accept");
                return;
            }

            waitAcitivityIndicator.IsRunning = true;
            enterButton.IsEnabled = false;

            HttpClient client = new HttpClient();

            /* Test with the webAPI 
            var authData = string.Format("{0}:{1}", userEntry.Text, userPassword.Text);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            */

            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");

            //passagem de parametros usando {0} e {1}, para username e a password
            string url = string.Format("/users/{0}", userEntry.Text);
            var response = await client.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;
            waitAcitivityIndicator.IsRunning = false;
            enterButton.IsEnabled = true;

            if(string.IsNullOrEmpty(result) || result == null || result == "{}")
            {
                await DisplayAlert("Error", "Username or Password incorrect", "Accept");
                userPassword.Text = string.Empty;
                userPassword.Focus();
                return;
            }
            else
            {
                await Navigation.PushModalAsync(new MenuPage());


            }


        }
    }
}
