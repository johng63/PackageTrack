using System;
using System.Collections.Generic;
using System.Text;

namespace PackageTrack.ViewModels
{
    public class LoginViewModel : BaseLoginViewModel
    {
        
        public LoginViewModel()
        {
            Username = "";
            Password = "";
            LoggedOnUser = "";
            PropertiesHelper props = new PropertiesHelper();
            isDBOnline = props.GetPropertyValue("DatabaseOnline");
        }

        string username = string.Empty;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        string loggedOnUser = string.Empty;
        public string LoggedOnUser
        {
            get { return loggedOnUser; }
            set { SetProperty(ref loggedOnUser, value); }
        }

        string isDBOnline = string.Empty;
        public string DBOnline
        {
            get { return isDBOnline; }
            set { SetProperty(ref isDBOnline, value); }
        }
    }
}
