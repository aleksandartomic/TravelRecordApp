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
        public MapPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need permission", "We will have to access your location for this", "Ok");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    locationsMap.IsShowingUser = true;
                    var locator = CrossGeolocator.Current;
                    locator.PositionChanged += Locator_PositionChanged;
                    await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);

                    var position = await locator.GetPositionAsync();

                    var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    var span = new MapSpan(center, 2, 2);
                    locationsMap.MoveToRegion(span);

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Post>();
                        var posts = conn.Table<Post>().ToList();

                        DisplayInMap(posts);
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("No permission", "You didn't grant permission for us to access your device's lcoation", "Ok");
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> posts)
        {
            List<Pin> pins = new List<Pin>();
            foreach (var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                { }
                catch (Exception ex)
                { }
            }
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}