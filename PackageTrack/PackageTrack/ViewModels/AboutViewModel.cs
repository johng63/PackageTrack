using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace PackageTrack.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            Username = Environment.UserName;
            Username1 = Environment.UserName;

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }
        string username1 = string.Empty;
        public string Username1
        {
            get { return username1; }
            set { SetProperty(ref username1, value); }
        }

        public ICommand OpenWebCommand { get; }
    }
}