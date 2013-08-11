using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes; 

using System.Xml.Linq;
using System.Device.Location;  
using System.Collections; 
using System.Collections.Generic;
using System.Linq; 

namespace CityTourApp
{
    public class LocationManager
    {
        private static WebClient wc = new WebClient();
        private static LocationData locData;
        private static LocationManager instance; 

        private LocationManager()
        {
            //wc = new WebClient();
            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted); 
        }

        public static LocationManager GetLocationManager 
        {
            get
            {

                if (instance == null)
                {
                    instance = new LocationManager();
                }
                return instance;
            }
        }
        
        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            XElement resultXml;

            locData = new LocationData(); 

            if (e.Error != null) { return; }

            else
            {
                XNamespace ns = "http://schemas.microsoft.com/search/local/ws/rest/v1";
                try
                {
                    resultXml = XElement.Load(e.Result);

                    locData.country = (string)(from el in resultXml.Descendants(ns + "CountryRegion")
                                       select el).First();
                    locData.city = (string)(from el in resultXml.Descendants(ns + "Locality")
                                    select el).First();
                    locData.address = (string)(from el in resultXml.Descendants(ns + "FormattedAddress") 
                                               select el).First();  

                }
                //catch (System.Xml.XmlException ex) 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    locData = null; 
                }
            }
        }


        public LocationData getLocationData(GeoCoordinate location)
        {
            wc.OpenReadAsync(new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=",
                                        location.Latitude, location.Longitude) + App.AppMapKey));

            if (locData != null) MessageBox.Show("Location data retrieved"); 
            return locData; 
        }
    }

    public class LocationData
    {
        public LocationData() { }

        public string country = "Country";
        public string city = "City";
        public string address = "Not available"; 
    }
}
