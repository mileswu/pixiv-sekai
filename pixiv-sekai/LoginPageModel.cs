using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pixiv_sekai
{


    class LoginPageModel	
    {
        // Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public SimpleRelayCommand LoginCommand { get; }

		// Constructor
		public LoginPageModel()
        {
			// Create commands
            LoginCommand = new SimpleRelayCommand(Login);
        }

        private void Login(object o)
        {
            Debug.WriteLine("Username = " + Username);
            Debug.WriteLine("Password = " + Password);
        }

    }
}
