using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class MainVM
    {
        public User User { get; set; }
        public LoginCommand LoginCommand { get; set; }

        public MainVM()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
        }
    }
}
