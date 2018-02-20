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
using Plugin.Connectivity;

namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        LoginViewModel loginViewModel;
        PropertiesHelper props;
        string server;


		public Login()
		{
			InitializeComponent ();
            BindingContext = loginViewModel = new LoginViewModel();
            UserDataStore uds = new UserDataStore();
            Pinger pinger = new Pinger();
            login_btn.IsEnabled = false;

            props = new PropertiesHelper();

            server = props.GetPropertyValue("Server");
            if (server == null || server.Length == 0)
            {
                props.SetPropertyValue("Server", "192.168.63.60");
            }

           IsConnected();

            //if (CrossConnectivity.Current.IsReachable("192.168.63.123").Result == false)
            //{
            //    props.SetPropertyValue("DatabaseOnline", "Offline");
            //    loginViewModel.DBOnline = "Offline";
            //    login_btn.IsEnabled = false;


            //}
            //else
            //{

            //    props.SetPropertyValue("DatabaseOnline", "Online");
            //    loginViewModel.DBOnline = "Online";
            //    login_btn.IsEnabled = true;

            //}

            //bool test = pinger.PingAddress("192.168.63.123");

            //if (test)
            //{
            //    props.SetPropertyValue("DatabaseOnline", "Online");
            //    loginViewModel.DBOnline = "Online";
            //    login_btn.IsEnabled = true;

            //}
            //else
            //{
            //    props.SetPropertyValue("DatabaseOnline", "Offline");
            //    loginViewModel.DBOnline = "Offline";
            //    login_btn.IsEnabled = false;

            //}
            //while (checkWait)
            //{

            //}

            loginViewModel.LoggedOnUser = "";
            start_btn.IsVisible = false;
            string userloggedin = props.GetPropertyValue("UserLoggedInUser") as string;


            if (userloggedin.Length > 0)
            {
                loginViewModel.LoggedOnUser = userloggedin + " is logged in.";

                start_btn.IsVisible = true;

            }


        }
        public async  void IsConnected()
        {
            Task<bool> task = checkConnectivity();


            bool test = await task;
            if (test)
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

        }

        private async Task<bool> checkConnectivity()
        {
            //return await Task.Run(async () =>
            //{
            //    bool mytest = await CrossConnectivity.Current.IsReachable("192.168.63.60");
            //    return mytest;
            //});

            bool testcon = true;
            try
            {
                testcon = await CrossConnectivity.Current.IsReachable(server);

                //if( await CrossConnectivity.Current.IsReachable(host.Text).Result == false)
                //{
                //    testcon = false;
                //}

                //Reachability.IsHostReachable("192.168.63.60");
            }
            catch (Exception e)
            {
                string message = e.Message;
            }


            return testcon;
        }
        private async Task<bool> checkConnectivity2()
        {


            var connectivity = CrossConnectivity.Current;
            if (!connectivity.IsConnected)
                return false;

            var reachable = await connectivity.IsRemoteReachable(server);

            return reachable;
        }

        private async void goto_Main(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());

        }
        async void Server_Clicked(object sender, EventArgs e)
        {
            server = props.GetPropertyValue("Server");
            string myinput = await InputBox(this.Navigation, server);
            if(myinput == null || myinput.Length == 0)
            {
               //display message - no input
            }
            else
            {
                props.SetPropertyValue("Server", myinput);
            }
            
            server = props.GetPropertyValue("Server");
            IsConnected();


        }

        public static Task<string> InputBox(INavigation navigation, string server)
        {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<string>();

            var lblTitle = new Label { Text = "Enter Server", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            var lblServer = new Label { Text = "Current Server IP is: " + server };
            var lblMessage = new Label { Text = "Enter new Server IP Address:" };
            var txtInput = new Entry { Text = "" };

            var btnOk = new Button
            {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                var result = txtInput.Text;
                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult(result);
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
            btnCancel.Clicked += async (s, e) =>
            {
                // close page
                await navigation.PopModalAsync();
                // pass empty result
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout
            {
                Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblTitle, lblServer, lblMessage, txtInput, slButtons },
            };

            // create and show page
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            server = props.GetPropertyValue("Server");
            IsConnected();

        }
        //async void OnAlertYesNoClicked(object sender, EventArgs e)
        //{

        //    var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        //   // Debug.WriteLine("Answer: " + answer);
        //}


    }
}