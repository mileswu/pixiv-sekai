using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;

namespace pixiv_sekai
{


    class LoginPageModel : INotifyPropertyChanged
    {
        // Properties
        public string _username = (App.Current as App).Pixiv.Username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public string _password = (App.Current as App).Pixiv.Password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public bool _loginInProgress = false;
        public bool LoginInProgress
        {
            get { return _loginInProgress; }
            set
            {
                _loginInProgress = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LoginInProgress"));
                }
            }
        }


        public SimpleRelayCommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor
        public LoginPageModel()
        {
			// Create commands
            LoginCommand = new SimpleRelayCommand(Login);
            LoginCommand.CanExecuteDelegate = x =>
                this.Password != null && this.Password != "" && this.Username != null && this.Username != "";

            // Auto-login if we can
            if(Username != null && Password != null)
            {
                Login(this);
            }
        }

        private async void Login(object o)
        {
            LoginInProgress = true;
            Task<bool> loginTask = (App.Current as App).Pixiv.Login(Username, Password);
            bool loginSuccessful = await loginTask;
            if (loginSuccessful)
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(RankingPage));
            }
            LoginInProgress = false;
        }

    }
}
