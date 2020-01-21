using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.airplane.png", assembly);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<User>();
                    var users = conn.Table<User>().ToList();

                    var user = (from u in users
                                where u.Email == emailEntry.Text
                                where u.Password == passwordEntry.Text
                                select u).ToList().FirstOrDefault();

                    if (user != null)
                    {
                        App.user = user;
                        if (user.Password == passwordEntry.Text)
                        {
                            Navigation.PushAsync(new HomePage());
                        }
                        else
                        {
                            DisplayAlert("Error", "Email or password are incorrect", "Ok");
                        }
                    }
                    else
                    {
                          DisplayAlert("Error", "There was an error logging you in", "Ok");
                    }
                }
            }
        }

        private void registerUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
