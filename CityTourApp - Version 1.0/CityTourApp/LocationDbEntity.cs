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


using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel; 



namespace CityTourApp
{
    [Table]
    public class LocationDbEntity : INotifyPropertyChanged, INotifyPropertyChanging 
    {
        private int _LocationItemId;                        ///column 1  

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int LocationItemId
        {
            get
            {
                return _LocationItemId;
            }
            set
            {
                if (_LocationItemId != value)
                {
                    NotifyPropertyChanging("LocationItemId");
                    _LocationItemId = value;
                    NotifyPropertyChanged("LocationItemId");
                }
            }
        }


        ///either "place" or "event". tells what kind of item this is 
        private string _Type;                            ///column 9

        [Column(CanBeNull = false)]
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    NotifyPropertyChanging("Type");
                    _Type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }


        private string _Country;                            ///column 2

        [Column(CanBeNull = false)]
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                if (_Country != value)
                {
                    NotifyPropertyChanging("Country");
                    _Country = value;
                    NotifyPropertyChanged("Country");
                }
            }
        }



        private string _City;                               ///column 3 

        [Column(CanBeNull = false)]
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                if (_City != value)
                {
                    NotifyPropertyChanging("City");
                    _City = value;
                    NotifyPropertyChanged("City");
                }
            }
        }



        private string _Name;                               ///column 4 

        [Column(CanBeNull = false)]
        public string Name 
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }



        private string _description;                               ///column 5  

        [Column(CanBeNull = false)]
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    NotifyPropertyChanging("Description");
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }



        private string _image_file;                               ///column 6  

        [Column]
        public string ImageFileStr
        {
            get
            {
                return _image_file;
            }
            set
            {
                if (_image_file != value)
                {
                    NotifyPropertyChanging("ImageFileStr");
                    _image_file = value;
                    NotifyPropertyChanged("ImageFileStr");
                }
            }
        }



        private double _lat;                               ///column 7  

        [Column(CanBeNull = false)]
        public double Lat
        {
            get
            {
                return _lat;
            }
            set
            {
                if (_lat != value)
                {
                    NotifyPropertyChanging("Lat");
                    _lat = value;
                    NotifyPropertyChanged("Lat");
                }
            }
        }



        private double _long;                               ///column 8   

        [Column(CanBeNull = false)]
        public double Long 
        {
            get
            {
                return _long;
            }
            set
            {
                if (_long != value)
                {
                    NotifyPropertyChanging("Long");
                    _long = value;
                    NotifyPropertyChanged("Long");
                }
            }
        }




        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

    }
}
