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
    public class UserDbEntity// : INotifyPropertyChanged, INotifyPropertyChanging  
    {
        private int _UserItemId;                        ///column 1 

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int UserItemId
        {
            get
            {
                return _UserItemId;
            }
            set
            {
                if (_UserItemId != value)
                {
                     //NotifyPropertyChanging("ToDoItemId");
                    _UserItemId = value;
                     //NotifyPropertyChanged("ToDoItemId");
                }
            }
        }


        private string _userName;                   ///column 2

        [Column(CanBeNull=false)]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    //NotifyPropertyChanging("ItemName");
                    _userName = value;
                    //NotifyPropertyChanged("ItemName");
                }
            }
        }


        private string _pWord;                   ///column 3 

        [Column(CanBeNull=false)]
        public string UserPassword
        {
            get
            {
                return _pWord;
            }
            set
            {
                if (_pWord != value)
                {
                    //NotifyPropertyChanging("ItemName");
                    _pWord = value;
                    //NotifyPropertyChanged("ItemName");
                }
            }
        }


        private string _photoStr;                   ///column 4 

        [Column]
        public string UserPhoto 
        {
            get
            {
                return _photoStr;
            }
            set
            {
                if (_photoStr != value)
                {
                    //NotifyPropertyChanging("ItemName");
                    _photoStr = value;
                    //NotifyPropertyChanged("ItemName");
                }
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;  
    }
}
