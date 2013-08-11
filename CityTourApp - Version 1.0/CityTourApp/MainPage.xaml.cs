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
using Microsoft.Phone.Shell; 

namespace CityTourApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        ApplicationBarIconButton btnSignOut, btnSignIn, btnAddItem, btnUserPage;

        public MainPage()
        {
            InitializeComponent();

            //create the application bar for the home screen 
            createApplicationBar(); 
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            createApplicationBar(); 
        }

        private void createApplicationBar()
        {
            ///application bar 
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 0.5;
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = true;

            ///the buttons
            //ApplicationBarIconButton 
            btnSignIn = new ApplicationBarIconButton();
            btnSignIn.IconUri = new Uri("/Icons/sign_in.png", UriKind.Relative);
            btnSignIn.Text = "sign in";
            //ApplicationBar.Buttons.Add(btnSignIn);
            btnSignIn.Click += new EventHandler(btnSignIn_Click);

            //ApplicationBarIconButton 
            btnSignOut = new ApplicationBarIconButton();
            btnSignOut.IconUri = new Uri("/Icons/sign_in.png", UriKind.Relative);
            btnSignOut.Text = "sign out";
            //ApplicationBar.Buttons.Add(btnSignOut); 
            btnSignOut.Click += new EventHandler(btnSignOut_Click);

            //ApplicationBarIconButton 
            btnAddItem = new ApplicationBarIconButton();
            btnAddItem.IconUri = new Uri("/Icons/add_img.png", UriKind.Relative);
            btnAddItem.Text = "add event/place";
            //ApplicationBar.Buttons.Add(btnAddItem); 
            btnAddItem.Click += new EventHandler(btnAddItem_Click);

            btnUserPage = new ApplicationBarIconButton();
            btnUserPage.IconUri = new Uri("/Icons/sign_in.png", UriKind.Relative);
            btnUserPage.Text = "user page";
            btnUserPage.Click += new EventHandler(btnbtnUserPage_Click);

            addButton(App.CurrentUser != null);

            ApplicationBarMenuItem menuItemSelectLoc = new ApplicationBarMenuItem();
            menuItemSelectLoc.Text = "Select Location";
            ApplicationBar.MenuItems.Add(menuItemSelectLoc);
            menuItemSelectLoc.Click += new EventHandler(menuItemSelect_Click); 
        }


        private void addButton(bool isSignedin) {
            if (isSignedin) { 
                    ApplicationBar.Buttons.Add(btnSignOut);
                    ApplicationBar.Buttons.Add(btnAddItem); 
            }
            else { ApplicationBar.Buttons.Add(btnSignIn); }
        }


        private void btnSignIn_Click(object sender, EventArgs e)
        {
            //Do work for your application here.
            NavigationService.Navigate(
                    new Uri("/Page_signIn.xaml", UriKind.Relative));
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            //Do work for your application here. 
            App.CurrentUser = null;
            MessageBox.Show("Message: User signed out.");       ///just feedback ;) 
            NavigationService.Navigate(
                    new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void btnAddItem_Click(object sender, EventArgs e) 
        {
            ///navigate to the add item page - Page_add_event_place.xaml 
            NavigationService.Navigate(
                    new Uri("/Page_add_event_place.xaml", UriKind.Relative));
        }

        private void btnbtnUserPage_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(
                    new Uri("/Page_user_home.xaml", UriKind.Relative));
        }

        private void menuItemSelect_Click(object sender, EventArgs e)
        {
            ///MessageBox.Show("Menu Item: select location clicked!");
            //Do work for your application here. 
            NavigationService.Navigate(
                    new Uri("/Page_choose_location_map.xaml", UriKind.Relative));
        }
    }
}
