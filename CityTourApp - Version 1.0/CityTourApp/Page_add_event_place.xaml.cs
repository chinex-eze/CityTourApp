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

using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage; 
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location; 


namespace CityTourApp
{
    public partial class Page_add_event_place : PhoneApplicationPage
    {
        private PhotoChooserTask photoChooserTask;
        private Pushpin pushpin { get; set; }
        private GeoCoordinate tempLoc { get; set; }
        private LocationData LocData { get; set; } 

        public Page_add_event_place()
        {
            InitializeComponent();

            //map_item.Tap += this.Map_TapEventHandler; 
            map_item.Hold += this.Map_DoubleTapEventHandler;

            photoChooserTask = new PhotoChooserTask(); 
            photoChooserTask.ShowCamera = true; 
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            pushpin = new Pushpin();
            pushpin.Tap += this.Pin_OnTap;
            map_item.Children.Add(pushpin);

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.DirectoryExists("Place")) store.CreateDirectory("Place");  
                if (!store.DirectoryExists("Event")) store.CreateDirectory("Event");  
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            confirmUser();
        }

        protected override void  OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
 	         base.OnNavigatedTo(e); 
             confirmUser();
        } 


        private void confirmUser() 
        {
            if (App.CurrentUser == null) ///user not signed in 
            {
                MessageBox.Show("You must be signed in!"); 
                NavigationService.Navigate( new Uri("/Page_signIn.xaml", UriKind.Relative)); 
            }
        }


        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }
        }


        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.OriginalFileName); ///complete file name 
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                { 
                    if (!store.DirectoryExists("temp")) ///for storing temporary files 
                    {
                        store.CreateDirectory("temp");
                    }

                    if (store.FileExists(@"temp\tempPhoto.jpg"))
                        store.DeleteFile(@"temp\tempPhoto.jpg");

                    IsolatedStorageFileStream fileStream = store.CreateFile(@"temp\tempPhoto.jpg");
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(e.ChosenPhoto);

                    int orientation = 0; int quality = 70;
                    WriteableBitmap wb = new WriteableBitmap(bitmap);
                    wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, orientation, quality); 

                    fileStream.Close();
                    MessageBox.Show("Image has been saved!"); 
                }
            }
            else { MessageBox.Show("Error: Photo was not chosen!"); }
        }



        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (validateEntries())
            {
                ///save to current item and naviate to the details page for it. 
                string itemName = txtName.Text; 
                string itemDesc = txtDesc.Text;
                string itemType = (rdoBtnPlace.IsChecked == true) ? "Place" : "Event"; 
                ///TODO: get the city and country values too 
                string city = LocData.city; 
                string country = LocData.country;
                string address = LocData.address; 

                string photoStr = ""; 

                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(@"temp\tempPhoto.jpg"))
                    {
                        photoStr = itemType + "/" + country + "_" + city + "_" + itemName + ".jpg";   //too long, huh? 
                        store.CopyFile(@"temp\tempPhoto.jpg", photoStr, true);        //true - overwrite if existing 
                        MessageBox.Show("Photo added.");
                    }
                }

                //create the location object and save it to the database  
                MessageBox.Show(LocData.address);
                LocationDbEntity newItem = new LocationDbEntity { 
                                                Type = itemType, 
                                                City = city,
                                                Country = country, 
                                                Description = itemDesc, 
                                                Name = itemName, 
                                                ImageFileStr = photoStr, 

                                                Lat = pushpin.Location.Latitude, 
                                                Long = pushpin.Location.Longitude
                                                };

                App.dbObjects.LocationItems.Add(newItem); 

                App.dbObjects.LocationItems.Add(newItem); 

                App.dbObjects.GetLocationDB.LocationItems.InsertOnSubmit(newItem); 

                App.dbObjects.GetLocationDB.SubmitChanges();  

                //set the current location 
                App.currentItem = newItem; 
                MessageBox.Show("item saved");
                NavigationService.Navigate(new Uri("/Page_details_map.xaml", UriKind.Relative)); 
            }
            else
            {
                MessageBox.Show("Error: Data not saved. \nInvalid Enries!"); 
            } 
        }



        private bool validateEntries()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                MessageBox.Show("Error: Empty fields!");
                return false; 
            }
            if (LocData == null)
            {
                MessageBox.Show("Error: Location data not available");
                return false; 
            }
            return true; 
        }



        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists("temp/tempPhoto.jpg"))
                    store.DeleteFile("temp/tempPhoto.jpg"); 
            }
        }



        void Pin_OnTap(object sender, System.Windows.Input.GestureEventArgs tapEvent)
        {
            ///get the current location data  
            btnDone.IsEnabled = false;

            LocData = (LocationManager.GetLocationManager).getLocationData(tempLoc); 
            
            if (LocData == null)
            {
                MessageBox.Show("ERROR: Location info could not be retrieved!");
                return; 
            }
            btnDone.IsEnabled = true; 
        }


        private void movePushpin(GeoCoordinate newLocation)
        {
            pushpin.Location = newLocation;
            map_item.SetView(pushpin.Location, 8);
        }

        void Map_DoubleTapEventHandler(object sender, System.Windows.Input.GestureEventArgs tapEvent)
        {
            Point point = tapEvent.GetPosition(null);

            //movePushpin(map_item.ViewportPointToLocation(point)); 
            tempLoc = map_item.ViewportPointToLocation(point);
            movePushpin(tempLoc); 
        }
    }
}