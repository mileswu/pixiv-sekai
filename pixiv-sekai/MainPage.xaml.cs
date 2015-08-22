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
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

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
            LoadRankings();
        }

        private async void LoadRankings()
        {
            Task<List<string> > task = (App.Current as App).Pixiv.Rankings();
            List<string> results = await task;

            foreach (string i in results)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(i);
                Image im = new Image();
                im.Source = bitmapImage;
                im.Height = 150;
                im.Width = 150;

                gridView.Items.Add(im);
            }
        }
    }
}
