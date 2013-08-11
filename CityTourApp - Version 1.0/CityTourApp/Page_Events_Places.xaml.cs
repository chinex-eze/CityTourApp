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

namespace CityTourApp
{
    public partial class Page_Events_Places : PhoneApplicationPage
    {
        public Page_Events_Places()
        {
            InitializeComponent();
            this.lstBoxEvents.DataContext = App.dbObjects.LocationItems; 
            this.lstBoxPlaces.DataContext = App.dbObjects.LocationItems;  
        }


        void DisplayDetail(object sender, SelectionChangedEventArgs args)
        {
            LocationDbEntity item = (sender as ListBox).SelectedItem as LocationDbEntity; 

            NavigationService.Navigate(new Uri("/Page_details_map.xaml?itemId=" + item.LocationItemId,
                                        UriKind.Relative));  
        }
    }
}