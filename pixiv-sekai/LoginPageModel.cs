using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pixiv_sekai
{


    class LoginPageModel	
    {
        // Properties
        public string Username { get; set; } = (App.Current as App).Pixiv.Username;
        public string Password { get; set; } = (App.Current as App).Pixiv.Password;
        public SimpleRelayCommand LoginCommand { get; }

		// Constructor
		public LoginPageModel()
        {
			// Create commands
            LoginCommand = new SimpleRelayCommand(Login);
        }

        private async void Login(object o)
        {
            Task<bool> loginTask = (App.Current as App).Pixiv.Login(Username, Password);
            bool loginSuccessful = await loginTask;
            if (loginSuccessful)
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(RankingPage));
            }
        }

    }
}
