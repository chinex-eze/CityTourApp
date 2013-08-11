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
    public partial class Page_signIn : PhoneApplicationPage
    {
        public Page_signIn()
        {
            InitializeComponent(); 

        }

        private void btnSign_in_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Error: Empty fields!");
                return;
            }

            if(App.validateLogin(txtUsername.Text, txtPassword.Password))
            {
                NavigationService.Navigate(new Uri("/Page_user_home.xaml", UriKind.Relative)); 
            }
            else
                MessageBox.Show("Error: Invalid password or username!");  
        }


        private void btnSign_up_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSUusername.Text) || //txtSUpassword
                string.IsNullOrWhiteSpace(txtSUpassword.Password))
            {
                MessageBox.Show("Error: Empty fields!"); 
                return;
            }

            if (App.createNewUser(txtSUusername.Text, txtSUpassword.Password) != null)
            {
                ///then navigate to the user page 
                NavigationService.Navigate(new Uri("/Page_user_home.xaml", UriKind.Relative)); 
            } 
            else 
                MessageBox.Show("Error: User already exists!");
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (App.CurrentUser != null)
            {
                NavigationService.Navigate(new Uri("/Page_user_home.xaml", UriKind.Relative));
            }
        }
    }
}