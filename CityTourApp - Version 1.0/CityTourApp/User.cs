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

namespace CityTourApp
{
    public class User
    {
        ///private constructor...users can only be created using the 
        ///factory method, createNewUser
        public User(string username, string pWord) 
        {
            Username = username;
            Password = pWord;
        }

        public string Username
        {
            get; set;
        }
        
        public string Password
        {
            get;
            private set;
        }

        //public Image UserPhoto { set; get; } 
        public string UserPhotoStr { set; get; }
    }
}
