using BiciMAD_Map.Models;
using BiciMAD_Map.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BiciMAD_Map
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Application.Current.Resuming += new EventHandler<Object>(App_Resuming);

            BiciMapControl.MapServiceToken = (string)(App.Current.Resources["MapServiceToken"]);

            // Center the map in Madrid
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 40.41792, Longitude = -3.705769 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            BiciMapControl.Center = cityCenter;
            BiciMapControl.ZoomLevel = 13;

            // Other UI initializations
            StationInfoPanel.Height = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainMapViewModel viewModel = (MainMapViewModel)DataContext;
            viewModel.FetchStationsExecute(null);

            if (StationInfoPanel.Height > 0)
                CloseStationInfoPanelAnimation.Begin();
        }

        private async void App_Resuming(Object sender, Object e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                MainMapViewModel viewModel = (MainMapViewModel)DataContext;
                viewModel.FetchStationsExecute(null);

                if (StationInfoPanel.Height > 0)
                    CloseStationInfoPanelAnimation.Begin();
            });
        }

        private void StationTapped(object sender, TappedRoutedEventArgs e)
        {
            Grid stationPin = sender as Grid;
            Station stationInfo = stationPin.DataContext as Station;

            StationInfoPanel.DataContext = stationInfo;

            if(StationInfoPanel.Height == 0)
                OpenStationInfoPanelAnimation.Begin();
        }

        private void refreshClick(object sender, RoutedEventArgs e)
        {
            MainMapViewModel viewModel = (MainMapViewModel)DataContext;
            viewModel.FetchStationsExecute(null);

            if(StationInfoPanel.Height > 0)
                CloseStationInfoPanelAnimation.Begin();
        }

        private async void getLocationClick(object sender, RoutedEventArgs e)
        {
            MainMapViewModel viewModel = (MainMapViewModel)DataContext;

            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    Geolocator geolocator = new Geolocator();

                    viewModel.IsLoading = true;

                    Geoposition pos = await geolocator.GetGeopositionAsync();

                    viewModel.IsLoading = false;

                    BasicGeoposition userPosition = new BasicGeoposition() { Latitude = pos.Coordinate.Latitude, Longitude = pos.Coordinate.Longitude };
                    Geopoint pointUserPostion = new Geopoint(userPosition);

                    // Add user location marker on the map
                    MapIcon userLocationIcon = new MapIcon();
                    userLocationIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/circle-location.png"));
                    userLocationIcon.Location = pointUserPostion;
                    userLocationIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    userLocationIcon.ZIndex = 0;

                    BiciMapControl.MapElements.Clear();
                    BiciMapControl.MapElements.Add(userLocationIcon);

                    // Zoom map to user location
                    await BiciMapControl.TrySetViewAsync(pointUserPostion, 17);

                    break;

                case GeolocationAccessStatus.Denied:
                    break;

                case GeolocationAccessStatus.Unspecified:
                    break;
            }
        }

        private async void moreZoomClick(object sender, RoutedEventArgs e)
        {
            await BiciMapControl.TryZoomInAsync();
        }

        private async void lessZoomClick(object sender, RoutedEventArgs e)
        {
            await BiciMapControl.TryZoomOutAsync();
        }
    }
}
