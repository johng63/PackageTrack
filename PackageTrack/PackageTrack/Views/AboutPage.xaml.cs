using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;


namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
            
            InitializeComponent ();
           
        }

        public AboutPage(string userName)
        {
            InitializeComponent();
        }
	}
}