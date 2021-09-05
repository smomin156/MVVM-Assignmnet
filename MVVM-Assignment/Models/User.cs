using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Assignment.Models
{
    public class User : INotifyPropertyChanged
    {
        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; onPropertyChanged("UserID"); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; onPropertyChanged("Name"); }
        }

        private long mobile;

        public long Mobile
        {
            get { return mobile; }
            set { mobile = value; onPropertyChanged("Mobile"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
