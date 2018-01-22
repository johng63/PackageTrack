using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PackageTrack.Views;
using Xamarin.Forms;

namespace PackageTrack
{
	public partial class App : Application
	{

        // update your Application ID or client ID 

        public static string ApplicationID = "----dfc69887-2089-4e8c-ssss-8d3591736a96";

        //modify your Azure tenant 

        public static string tenanturl = "https://login.microsoftonline.com/<Azure";       

       //Update your return url 

        public static string ReturnUri = "http://DevEnvAzure.microsoft.net";

        //No need to change 

        public static string GraphResourceUri = "https://graph.microsoft.com";

        public static AuthenticationResult AuthenticationResult = null;

        public App ()
		{
			InitializeComponent();

            MainPage = new MainPage();

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
