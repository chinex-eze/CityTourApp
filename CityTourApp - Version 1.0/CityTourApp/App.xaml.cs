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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.ComponentModel;
using System.Collections.ObjectModel; 

namespace CityTourApp
{
    public partial class App : Application, INotifyPropertyChanged
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        ///holds the current location or item
        public static LocationDbEntity currentItem { get; set; } 

        ///stores the current user data
        public static UserDbEntity CurrentUser { get; set; } 

        ///bing app map key
        public static string AppMapKey = "AnfL8v-q0dcsrMhS-UpUyBGWIbmwqaTaOuQBj9gJFuHCL0gT0hDlXRzUzdapKAhS";

        /// the users database object 
        private static UserDataContext Userdb = null; 

        ///list of users...
        private static List<UserDbEntity> users = null;

        public static DBObjects dbObjects; 

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            // Note that exceptions thrown by ApplicationBarItem.Click will not get caught here.
            UnhandledException += Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            createUserDB();  
            //createLocationDB(); 
            dbObjects = new DBObjects(); 
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            Userdb.Dispose(); 
        } 

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger 
                System.Diagnostics.Debugger.Break();
            }
        } 


        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

        /// <summary>
        /// application menu events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, EventArgs e)
        {
            PhoneApplicationFrame root = 
                    Application.Current.RootVisual as PhoneApplicationFrame; 
            root.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Exit button!");
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("about menu: Program still under construction!");
            PhoneApplicationFrame root =
                    Application.Current.RootVisual as PhoneApplicationFrame; 
            root.Navigate(new Uri("/Page_about.xaml", UriKind.Relative)); 
        }


        /// <summary>
        ///     user thingy's
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pWord"></param>
        /// <returns></returns>
        public static UserDbEntity createNewUser(string username, string pWord)
        {
            if (isUserExist(username))
            {
                return null;
            }
            ///else create the user...
            UserDbEntity user = new UserDbEntity { UserName=username, UserPassword=pWord };
            
            users.Add(user); 
            Userdb.UserItems.InsertOnSubmit(user); 
            Userdb.SubmitChanges();

            CurrentUser = user;

            return user;
        }

        private static bool isUserExist(string username)
        {
            foreach (UserDbEntity user in users)
            {
                if (username == user.UserName)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool validateLogin(string username, string password) 
        {
            foreach (UserDbEntity user in users)
            {
                if (user.UserName == username &&
                    user.UserPassword == password)
                {
                    CurrentUser = user;
                    return true; 
                }
            }
            return false; 
        } 

        /// not needed...
        public static UserDbEntity getUser(string username, string password)
        {
            foreach (UserDbEntity user in users)
            {
                if (user.UserName == username &&
                    user.UserPassword == password)
                {
                    CurrentUser = user;
                    return user;
                }
            }
            return new UserDbEntity(); 
        }


        private void createUserDB()
        {
            // Create the database if it does not exist.
            //using (UserDataContext Userdb = new UserDataContext(UserDataContext.DBConnectionString))
            //{ 
            Userdb = new UserDataContext(UserDataContext.DBConnectionString);
                if (Userdb.DatabaseExists() == false)
                {
                    //Create the database
                    Userdb.CreateDatabase();
                }
                loadUsers();
            //}
        }

        private void loadUsers()
        {
            // Define the query to gather all of the to-do items. 
            var userItemsInDB = from UserDbEntity user in Userdb.UserItems
                                select user;
            // Execute the query and place the results into a collection.
            users = new List<UserDbEntity>(userItemsInDB); 
        } 


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion  
    }






    public class DBObjects : INotifyPropertyChanged 
    {
        private LocationDataContext LocationDB;

        // Define an observable collection property that controls can bind to.
        private static ObservableCollection<LocationDbEntity> _LocationItems; 
        
        public DBObjects()
        {
            // Connect to the database and instantiate data context.
            LocationDB = new LocationDataContext(LocationDataContext.DBConnectionString); 
            createLocationDB();
            loadLocationItems(); 
        }


        ~DBObjects()
        {
            LocationDB.Dispose(); 
        }


        private void createLocationDB()
        {
            // Create the database if it does not exist.
            using (LocationDataContext LocationDb = new LocationDataContext(LocationDataContext.DBConnectionString))
            {
                if (LocationDb.DatabaseExists() == false)
                {
                    //Create the database
                    LocationDb.CreateDatabase();
                }
            }

        }


        public LocationDbEntity GetItemFromCollection(int itemId)
        {
            foreach (LocationDbEntity item in _LocationItems)
            {
                if (itemId == item.LocationItemId)
                {
                    return item;
                }
            }

            return null; 
        }


        //private ObservableCollection<LocationDbEntity> _LocationItems;
        public ObservableCollection<LocationDbEntity> LocationItems
        {
            get
            {
                return _LocationItems;
            }
            set
            {
                if (_LocationItems != value)
                {
                    _LocationItems = value;
                    NotifyPropertyChanged("LocationItems");
                }
            }
        }

        private void loadLocationItems()
        {
            // Define the query to gather all of the to-do items.
            var locationItemsInDB = from LocationDbEntity locationItem in LocationDB.LocationItems
                                    select locationItem;

            // Execute the query and place the results into a collection.
            LocationItems = new ObservableCollection<LocationDbEntity>(locationItemsInDB);
        }


        public LocationDataContext GetLocationDB
        {
            get { return LocationDB; }
            private set { LocationDB = value; }
        }  


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}