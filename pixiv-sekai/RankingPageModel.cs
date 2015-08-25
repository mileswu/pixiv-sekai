﻿using System;
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
        public bool HasMoreItems { get; } = true;
        private int PageNumber { get; set; } = 1;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
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