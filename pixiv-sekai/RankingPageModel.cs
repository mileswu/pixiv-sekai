using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace pixiv_sekai
{
    class RankingPageModel
    {
        public ObservableCollection<string> Works { get; }

        public RankingPageModel()
        {
            Works = new ObservableCollection<string>();
            LoadRankings();
        }

        private async void LoadRankings()
        {
            List<string> results = await (App.Current as App).Pixiv.Rankings();

            foreach (string i in results)
            {
                Works.Add(i);
            }
        }
    }
}
