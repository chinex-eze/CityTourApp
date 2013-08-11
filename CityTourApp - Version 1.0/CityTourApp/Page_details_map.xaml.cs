using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using System.Windows.Data;  
using System.Windows.Media.Imaging;  
using System.IO.IsolatedStorage;    
using System.IO; 

namespace CityTourApp
{
    public partial class Page_details_map : PhoneApplicationPage 
    {
        public Page_details_map()
        {
            InitializeComponent(); 
        }

        void DisplayDefaultItem()
        {
            txtName.DataContext = App.currentItem;
            imgPhoto.DataContext = App.currentItem;
            txtDesc.DataContext = App.currentItem;  
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string itemIdStr = "";
            int itemId;
            if (NavigationContext.QueryString.TryGetValue("itemId", out itemIdStr))
            {
                Int32.TryParse(itemIdStr, out itemId);
                LocationDbEntity item = App.dbObjects.GetItemFromCollection(itemId);
                if (item == null)
                {
                    MessageBox.Show("Invalid selected item."); 
                }
                else
                {
                    txtName.DataContext = item;
                    imgPhoto.DataContext = item;
                    txtDesc.DataContext = item;
                }
            }
            else
                DisplayDefaultItem(); 
        }
    }



    public class StringToImageConverter : IValueConverter 
    {
        /// Not yet fully implemented 
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
                                            System.Globalization.CultureInfo culture)
        {
            string imgPath = value as string;
            BitmapImage image; 
            //TODO: image loading routine... 
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream = store.OpenFile(imgPath, FileMode.Open))
                {
                    image = new BitmapImage();
                    image.SetSource(isoStream);
                }
            }

            return image; 
        }


        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter,
                                        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); 
        }

        #endregion 
    }
}