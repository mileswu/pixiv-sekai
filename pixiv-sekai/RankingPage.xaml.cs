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
    public sealed partial class RankingPage : Page
    {
        public RankingPage()
        {
            this.InitializeComponent();
        }

        private void GridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            int desiredSize = 150;
            int availableWidth = (int) (e.NewSize.Width - gridView.Padding.Right - gridView.Padding.Left);
            int numColumns = availableWidth / desiredSize;
            int width = (int)((float)availableWidth) / numColumns;

            var itemsWrapGrid = (ItemsWrapGrid)gridView.ItemsPanelRoot;
            itemsWrapGrid.ItemWidth = width;
            itemsWrapGrid.ItemHeight = width;
        }

        private void modeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mode = ((TextBlock)modeComboBox.SelectedItem).Text.ToLower();
            ((RankingPageModel)DataContext).Works.Mode = mode;
        }
    }
}
