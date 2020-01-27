using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            BindingContext = selectedPost;

            this.selectedPost = selectedPost;

            //experienceEntry.Text = selectedPost.Experience;
            //venueLabel.Text = selectedPost.VenueName;
            //categoryLabel.Text = selectedPost.CategoryName;
            //addressLabel.Text = selectedPost.Address;
            
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Post>();
                connection.Table<Post>();
                int rows = connection.Update(selectedPost);
                connection.Close();

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience succesfully updated", "Ok");
                    Navigation.PushAsync(new HomePage());
                }
                else
                {
                    DisplayAlert("Failure", "Experience failed to be updated", "Ok");
                }
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Post>();
                int rows = connection.Delete(selectedPost);
                connection.Close();

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience succesfully deleted", "Ok");
                    Navigation.PushAsync(new HomePage());
                }
                else
                    DisplayAlert("Failure", "Experience failed to be deleted", "Ok");
            }
        }
    }
}