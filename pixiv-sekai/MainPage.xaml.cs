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
using System.Net;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace pixiv_sekai
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebRequest webRequest = WebRequest.Create("https://oauth.secure.pixiv.net/auth/token");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            string postDataString = "username=" + usernameTextBox.Text + "&password=" + passwordTextBox.Text + "&grant_type=password&client_id=bYGKuGVw91e0NMfPGp44euvGt59s&client_secret=HP3RmkgAmEGro0gn1x9ioawQE8WMfvLXDz3ZqxpK";
            byte[] postDataBytes = Encoding.UTF8.GetBytes(postDataString);
            using (Stream newStream = await webRequest.GetRequestStreamAsync())
            {
                await newStream.WriteAsync(postDataBytes, 0, postDataBytes.Length);
            }

            string responseBody;
            try
            {
                WebResponse webResponse = await webRequest.GetResponseAsync();
                responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            }
            catch (WebException exception)
            {
                WebResponse webResponse = exception.Response;
                responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                textBlock.Text = "ERROR";
                Debug.WriteLine("ERROR");
                Debug.WriteLine(responseBody);
                return;
            }

            JObject responseJSON = JObject.Parse(responseBody);

            string token = (string)responseJSON["response"]["access_token"];
            int lifetime = (int)responseJSON["response"]["expires_in"];
            textBlock.Text = "Logged in...";
            Debug.WriteLine("Obtained token (token = " + token + ", lifetime = " + lifetime + ")");
        }
    }
}
