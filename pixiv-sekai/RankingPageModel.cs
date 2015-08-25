using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
#if DEBUG
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return new Func<Task<LoadMoreItemsResult>>(async () =>
                {
                    Add("Assets/StoreLogo.png");
                    return new LoadMoreItemsResult { Count = 1 };
                })().AsAsyncOperation();
#endif

            return new Func<Task<LoadMoreItemsResult>>(async () =>
            {
                List<string> results = await (App.Current as App).Pixiv.Rankings(PageNumber);
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
    }
}
