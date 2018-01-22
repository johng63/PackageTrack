using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
            
            InitializeComponent ();
            UserInfo user1 = new UserInfo();
            
        }

        public AboutPage(string userName)
        {
            InitializeComponent();
        }
	}
}