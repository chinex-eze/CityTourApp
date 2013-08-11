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
using System.Device.Location;

namespace CityTourApp
{
    public abstract class LocationItem
    {
        public GeoCoordinate Coordinate { get; set; }
        public string Country { get; set; }
        public string City { get; set; } 

        public string Name { get; set; }
        public string Description { get; set; }
        public Image Picture { get; set; }      ///use Image newImage = Image.FromFile("SampImag.jpg"); 
                                                ///which means that only the name of image file is stored in 
                                                ///the database 

        protected LocationItem(double lat, double lon, string name) 
        {
            Coordinate = new GeoCoordinate(lat, lon);
            Name = name; 
        }
    }
}
