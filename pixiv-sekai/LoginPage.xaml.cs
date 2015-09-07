using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace pixiv_sekai
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            // Fill initial passwordBox with saved password from DataContext/ViewModel
            passwordBox.Password = ((LoginPageModel)this.DataContext).Password;
        }

        // Update DataContext/ViewModel with password from passwordBox
        // This is necessary because a passwordBox can't bind directly to the DataContext/ViewModel
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginPageModel)this.DataContext).Password = passwordBox.Password;
        }
    }
}
