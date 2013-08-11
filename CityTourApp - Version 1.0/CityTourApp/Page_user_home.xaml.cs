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
using System.Windows.Navigation; 

namespace CityTourApp
{
    public partial class Page_user_home : PhoneApplicationPage
    {
        public Page_user_home()
        {
            InitializeComponent();

            pvtCtrl_userHome.DataContext = "Hi, " + App.CurrentUser.UserName; 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) 
        {
            base.OnNavigatedTo(e);
            if (App.CurrentUser == null)
            {
                MessageBox.Show("Error: User not signed in."); 
                NavigationService.Navigate(
                    new Uri("/Page_signIn.xaml", UriKind.Relative));
                return;
            }
                MessageBox.Show("Hi there, " + App.CurrentUser.UserName); 
                txtUserName.Text = App.CurrentUser.UserName + " home.";
        }
    }
}