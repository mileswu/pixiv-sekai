using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public sealed partial class RankingPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<string> ModeItems { get; } = new ObservableCollection<string>();
        private int _modeSelectedIndex = 0;
        public int ModeSelectedIndex
        {
            get { return _modeSelectedIndex; }
            set
            {
                _modeSelectedIndex = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ModeSelectedIndex"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RankingPage()
        {
            ModeItems.Add("Daily");
            ModeItems.Add("Weekly");
            ModeItems.Add("Monthly");
            ModeItems.Add("Rookie");
            AddExtraModes();

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
            var mode = ((string)modeComboBox.SelectedItem).ToLower();
            ((RankingPageModel)DataContext).Works.Mode = mode;
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = (string)((TextBlock)categoryComboBox.SelectedItem).Tag;

            if(type == "all")
            {
                AddExtraModes();
            }
            else
            {
                RemoveExtraModes();
            }

            ((RankingPageModel)DataContext).Works.Type = type;
        }

        private void AddExtraModes()
        {
            if(ModeItems.Contains("Original") == false)
                ModeItems.Add("Original");
            if (ModeItems.Contains("Male") == false)
                ModeItems.Add("Male");
            if (ModeItems.Contains("Female") == false)
                ModeItems.Add("Female");
        }

        private void RemoveExtraModes()
        {
            var sel = (string)modeComboBox.SelectedItem;
            if(sel == "Original" || sel == "Male" || sel == "Female")
            {
                ModeSelectedIndex = 0;
            }

            ModeItems.Remove("Original");
            ModeItems.Remove("Male");
            ModeItems.Remove("Female");
        }
    }
}
