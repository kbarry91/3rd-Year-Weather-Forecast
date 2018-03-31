// Author : G00339811
// Module : Mobile Application Developement 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.UI.Notifications;

namespace WeatherForecast
{
    /*
     Mainpage is the base page loaded when app is run.
     */
    public sealed partial class MainPage : Page
    {

        public String UserCity { get; set; }
        public String LocationForecast { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        /*
         * CityButton is the main search button .
         * Search must be saved and added to local storage.
         */
        private void CityButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

            String userText = cityInput.Text;

            // Play a sound effect.
            App.MyAppSounds.Play(SoundEfxEnum.CLICK);

            // Error checking in invalid city entered.
            if (userText.Length < 3)
            {
                errorBox.Visibility = Visibility;
            }
            else
            {
                String citySearch = "q=" + userText;
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                // Save the search and date in local storage.
                localSettings.Values["prevSearch"] = userText + " on " + DateTime.Now.ToString("M/d/yyyy");
                Frame.Navigate(typeof(WeatherPage), citySearch);
            }

        }

        private async void GetLocation_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Request permission from user for location.
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                // If permission granted recieve location and display a button to check forecast.
                case GeolocationAccessStatus.Allowed:
                    ShowToastNotification("Loading", "Waiting for update");

                    var geoLocator = new Geolocator
                    {
                        DesiredAccuracy = PositionAccuracy.High
                    };
                    var pos = await geoLocator.GetGeopositionAsync();

                    string latitude = pos.Coordinate.Point.Position.Latitude.ToString();
                    string longitude = pos.Coordinate.Point.Position.Longitude.ToString();

                    userLocation.Visibility = Visibility;

                    var userLocationStr = latitude + "\n" + longitude;
                    userLocation.Text = userLocationStr;

                    // Display button for current location forecast.
                    this.LocationForecast = "lat=" + latitude + "&lon=" + longitude;
                    getLocForecast.Visibility = Visibility;

                    ShowToastNotification("Success", "Location updated");
                    break;

                case GeolocationAccessStatus.Denied:
                    ShowToastNotification("Denied", "Access to location is denied.");
                    break;

                case GeolocationAccessStatus.Unspecified:
                    ShowToastNotification("Error", "Unspecified error.");
                    break;
            }

        }

        /*
         * Tooast notifications to show various success , progress or failures.
         */
        private void ShowToastNotification(string title, string stringContent)
        {
            // Initialize a ToastNotifier.
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");

            // Add a title and a notification body.
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");

            // Set audio property to play on notification.
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml)
            {
                // Set the notification to disappeaar after 4 seconds.

                ExpirationTime = DateTime.Now.AddSeconds(4)
            };

            // Display the toast.
            ToastNotifier.Show(toast);
        }

        private void GetLocForecast_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // Save the search and date in local storage.
            localSettings.Values["prevSearch"] = LocationForecast + " on " + DateTime.Now.ToString("M/d/yyyy");
            Frame.Navigate(typeof(WeatherPage), LocationForecast);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Load local storage.
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // If local storage value is available show the prevSearch textbox with date and last search.
            // Must also set the temperature setting.
            try
            {
                var VisibilityPropertyA = Visibility.Visible;
                prevSearch.Text = "Your last forecast was for " + (string)localSettings.Values["prevSearch"];
                prevSearch.Visibility = VisibilityPropertyA;


                var chosen = (string)localSettings.Values["tempSetting"];
                // Set default selected based on local storage.
                if (chosen == "Kelvin")
                {
                    tempType.SelectedIndex = 1;
                }
                else
                {
                    tempType.SelectedIndex = 0;
                }
            }
            catch
            {
                // If no local storage value is available do not show the prevSearch textbox and set defaults.
                prevSearch.Text = "No recent searches";
                var VisibilityPropertyA = Visibility.Collapsed;
                prevSearch.Visibility = VisibilityPropertyA;
                tempType.SelectedIndex = 0;
            }

            base.OnNavigatedTo(e);
        }

        /*
         * Drop down box to choose tempeture format, Chosen tempeture will be saved to local storage.
        */
        private void TempType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Play a sound effect.
            App.MyAppSounds.Play(SoundEfxEnum.CLICK);

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;
            if (comboBoxItem == null) return;
            var content = comboBoxItem.Content as string;
            if (content != null && content.Equals("Celsius"))
            {
                // Save the choice in local storage.
                localSettings.Values["tempSetting"] = content;
            }
            if (content != null && content.Equals("Kelvin"))
            {
                localSettings.Values["tempSetting"] = content;
            }
        }
    }
}
