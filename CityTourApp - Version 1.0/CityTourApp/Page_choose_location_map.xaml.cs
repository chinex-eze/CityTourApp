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
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using System.Xml.Linq;
using System.Windows.Media.Imaging; 


namespace CityTourApp
{
    public partial class Page_choose_location_map : PhoneApplicationPage
    {
        private GeoCoordinate currentLocation { get; set; }
        private Pushpin currPushpin { get; set; }
        
        public Page_choose_location_map()
        {
            InitializeComponent();

            ///add an event handler to handle the tap event on the map 
            //map_choose_location.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(MapTapHandler); 
            ///I would have loved to use the tap event for this but then even when the zoom is intended 
            ///with the zoom icons, the tap event is raised
            map_choose_location.Hold += this.Map_HoldEventHandler;
            //currPushpin.Tap += this.Pin_OnTap; 

            setCurrentLocation();
            currPushpin = new Pushpin();
            currPushpin.Tap += this.Pin_OnTap; 

            map_choose_location.Children.Add(currPushpin); 
            movePushpin(currentLocation);

            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted); 
            //getLocationData();
        }

        private void setCurrentLocation()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

            currentLocation = watcher.Position.Location;

            //GeoCoordinate(60.449249, 22.259239);  //ICT house Turku  
        }


        private void movePushpin(GeoCoordinate newLocation)  
        {
            currPushpin.Location = newLocation; 
            map_choose_location.SetView(currPushpin.Location, 10); 
        }

        void Map_HoldEventHandler(object sender, System.Windows.Input.GestureEventArgs tapEvent) 
        { 
            Point point = tapEvent.GetPosition(null); 

            movePushpin( map_choose_location.ViewportPointToLocation(point)); 
            currentLocation = map_choose_location.ViewportPointToLocation(point);
            getLocationData(); 
            //movePushpin(new GeoCoordinate(60.449249, 22.259239)); 
        }

        /*void Pin_OnTap(object sender, System.Windows.Input.GestureEventArgs tapEvent) 
        {
            CivicAddressResolver resolver = new CivicAddressResolver();
            //CivicAddress address = resolver.ResolveAddress(currPushpin.Location); 
            CivicAddress address = resolver.ResolveAddress(new GeoCoordinate(60.44, 22.25));

            if (address.IsUnknown) MessageBox.Show("address is unknown");
            else MessageBox.Show("gotcha"); 
            //NavigationService.Navigate(new Uri("/Page_Events_Places.xaml", UriKind.Relative));
        }*/

        WebClient wc = new WebClient(); 

        void Pin_OnTap(object sender, System.Windows.Input.GestureEventArgs tapEvent)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Location information not available"); 
                return;
            }
            ///sends location data in the form: country/city
            NavigationService.Navigate(new Uri("/Page_Events_Places.xaml?loc=" + country + 
                                "/" + city, 
                                UriKind.Relative));
        }

        string country = String.Empty, city = String.Empty;
        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e) 
        {
            XElement resultXml; 

            if (e.Error != null) { return; }

            else 
            {
                XNamespace ns = "http://schemas.microsoft.com/search/local/ws/rest/v1"; 
                try
                {
                    resultXml = XElement.Load(e.Result);
                    //string country="", city = "";
                    country = (string)(from el in resultXml.Descendants(ns + "CountryRegion")
                                           select el).First();
                    city = (string)(from el in resultXml.Descendants(ns + "Locality")
                                        select el).First();

                    PageTitle.Text = city + ", " + country;  
                }
                //catch (System.Xml.XmlException ex) 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            }
        }

        void getLocationData()
        {
            string mapKey = App.AppMapKey;
            wc.OpenReadAsync(new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=", 
                                        currentLocation.Latitude, currentLocation.Longitude) + mapKey));
        }
    }
}