using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class User : INotifyPropertyChanged
    {
        private string id;
        [PrimaryKey, AutoIncrement]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnProperyChanged("Id");
            }
        }

        private string email;
        [Required]
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnProperyChanged("Email");
            }
        }

        private string password;
        [Required]
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnProperyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnProperyChanged("ConfirmPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProperyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //public static async Task<bool> Login(string email, string password)
        //{
        //    bool isEmailEmpty = string.IsNullOrEmpty(email);
        //    bool isPasswordEmpty = string.IsNullOrEmpty(password);

        //    if (isEmailEmpty || isPasswordEmpty)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
        //            {
        //                conn.CreateTable<User>();
        //                var users = conn.Table<User>().ToList();

        //                var user = (from u in users
        //                            where u.Email == email
        //                            where u.Password == password
        //                            select u).ToList().FirstOrDefault();

        //                if (user != null)
        //                {
        //                    App.user = user;
        //                    if (user.Password == password)
        //                    {
        //                        return true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }

        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}

