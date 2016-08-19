using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Data;
using Windows.Foundation.Collections;
using Windows.Foundation;

namespace pixiv_sekai
{
    class WorksCollection : ObservableCollection<string>, ISupportIncrementalLoading
    {
        public bool HasMoreItems
        {
            get
            {
                if (PageNumber <= 10) return true;
                else return false;
            }
        }
        private int PageNumber { get; set; } = 1;

        private string _mode = "daily";
        public string Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                PageNumber = 1;
                Clear();
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
#if DEBUG
            // Placeholder for XAML Design Mode, since actual load task won't work
#pragma warning disable 1998
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return new Func<Task<LoadMoreItemsResult>>(async () =>
                {
                    Add("Assets/StoreLogo.png");
                    return new LoadMoreItemsResult { Count = 1 };
                })().AsAsyncOperation();
#pragma warning restore 1998
#endif

            return new Func<Task<LoadMoreItemsResult>>(async () =>
            {
                List<string> results = await (App.Current as App).Pixiv.Rankings(Mode, PageNumber);
                PageNumber += 1;

                foreach (string i in results)
                {
                    Add(i);
                }

                return new LoadMoreItemsResult { Count = (uint)results.Count };
            })().AsAsyncOperation();
        }
    }

    class RankingPageModel
    {
        public WorksCollection Works { get; } = new WorksCollection();

        public SimpleRelayCommand LogoutCommand { get; }

        public RankingPageModel()
        {
            // Create commands
            LogoutCommand = new SimpleRelayCommand(Logout);
        }

        private async void Logout(object o)
        {
            Task<bool> logoutTask = (App.Current as App).Pixiv.Logout();
            await logoutTask;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginPage));
        }
    }
}
