using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        
        private int id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set 
            {   id = value;
                OnProperyChanged("Id");
            }
        }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set 
            {   experience = value;
                OnProperyChanged("Experience");
            }
        }

        private string venueName;
        public string VenueName
        {
            get { return venueName; }
            set 
            {   venueName = value;
                OnProperyChanged("VenueName");
            }
        }

        private string categoryId;
        public string CategoryId
        {
            get { return categoryId; }
            set 
            {   categoryId = value;
                OnProperyChanged("CategoryId");
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set 
            { categoryName = value;
              OnProperyChanged("CategoryName");
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set 
            {   address = value;
                OnProperyChanged("Address");
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set 
            {   latitude = value;
                OnProperyChanged("Latitude");
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set 
            {   longitude = value;
                OnProperyChanged("Longitude");
            }
        }
        public string LatLong
        {
            get
            {
                return string.Format("{0} - {1}", Latitude.ToString("#.##"), Longitude.ToString("#.##"));
            }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
            set 
            {   distance = value;
                OnProperyChanged("Distance");
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set 
            {   userId = value;
                OnProperyChanged("UserId");
            }
        }

        private DateTimeOffset createAt;

        public DateTimeOffset CreateAt
        {
            get { return createAt; }
            set 
            { 
                createAt = value;
                OnProperyChanged("CreateAt");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProperyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
