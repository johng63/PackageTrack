using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PackageTrack.Models;
using PackageTrack.Services;
using PackageTrack.ViewModels;
using PackageTrack.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        LoginViewModel loginViewModel;
        PropertiesHelper props;
        UserDataStore uds;

		public Login()
		{
			InitializeComponent ();
            BindingContext = loginViewModel = new LoginViewModel();
            UserDataStore uds = new UserDataStore();
            Pinger pinger = new Pinger();

            props = new PropertiesHelper();

            loginViewModel.LoggedOnUser = "";
            start_btn.IsVisible = false;
            string userloggedin = props.GetPropertyValue("UserLoggedInUser") as string;

            if (pinger.PingAddress("192.168.63.60"))
            {
                props.SetPropertyValue("DatabaseOnline", "Online");
                loginViewModel.DBOnline = "Online";
                login_btn.IsEnabled = true;

            }
            else
            {
                props.SetPropertyValue("DatabaseOnline", "Offline");
                loginViewModel.DBOnline = "Offline";
                login_btn.IsEnabled = false;

            }

            if (userloggedin.Length > 0)
            {
                loginViewModel.LoggedOnUser = userloggedin + " is logged in.";

                start_btn.IsVisible = true;

            }


        }

        private async void goto_Main(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());

        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            
            UserDataStore uds = new UserDataStore();
            User user = await uds.GetItemAsync(loginViewModel.Username);
            if (user != null && user.UserName.Length > 0)
            {
                if (loginViewModel.Password.Equals(user.Password))
                {
                    loginViewModel.LoggedOnUser = user.UserName + " you are now logged in.";
                    loginViewModel.Username = "";
                    loginViewModel.Password = "";
                    props.SetPropertyValue("UserLoggedInUser", user.UserName);
                    start_btn.IsVisible = true;

                    await Navigation.PushAsync(new MainPage());

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Wrong Credentials", "Please Enter again", "OK");
                    loginViewModel.LoggedOnUser = "";
                    props.SetPropertyValue("UserLoggedInUser", "");
                    start_btn.IsVisible = false;

                }
            }
        }

        //async void OnAlertYesNoClicked(object sender, EventArgs e)
        //{

        //    var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        //   // Debug.WriteLine("Answer: " + answer);
        //}


    }
}