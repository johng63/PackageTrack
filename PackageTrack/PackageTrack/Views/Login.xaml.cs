using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
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

		public Login()
		{
			InitializeComponent ();
            BindingContext = loginViewModel = new LoginViewModel();
            loginViewModel.LoggedOnUser = "";
           
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            
            UserDataStore uds = new UserDataStore();
            User user = await uds.GetItemAsync("jfgianni");
            if (user.UserName.Length > 0)
            {
                if (loginViewModel.Password.Equals(user.Password))
                {
                    loginViewModel.LoggedOnUser = user.UserName + " you are now logged in.";
                    loginViewModel.Username = "";
                    loginViewModel.Password = "";
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Wrong Credentials", "Please Enter again", "OK");
                    loginViewModel.LoggedOnUser = "";

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