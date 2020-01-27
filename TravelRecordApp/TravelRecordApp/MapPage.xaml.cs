using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator.Abstractions;
using TravelRecordApp.Model;
using SQLite;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool hasLocationPermission = false;
        public MapPage()
        {
            InitializeComponent();
            GetPermissions();
        }
        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need your location", "We need to access your location", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    locationsMap.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Location denied", "You didn't give us permission to access location, so we can't show you where you are", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveMap(position);
            }
        }
        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }

        protected async override void OnAppearing()
        {
            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 100);

            var position = await locator.GetPositionAsync();

            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();

                DisplayInMap(posts);
            }
        }

        protected override async void OnDisappearing()
        {
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}