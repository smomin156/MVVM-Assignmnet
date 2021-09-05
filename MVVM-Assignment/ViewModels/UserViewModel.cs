using MVVM_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MVVM_Assignment.Commands;

namespace MVVM_Assignment.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private User DomObj = new User();
        private ObservableCollection<User> _UserList;
        public ObservableCollection<User> Users
        {
            get { return _UserList; }
            set { _UserList = value; }
        }
        public int UserID
        {
            get { return DomObj.UserID; }
            set { DomObj.UserID = Convert.ToInt32(value); onPropertyChanged("UserID"); }
        }
        public string Name
        {
            get { return DomObj.Name; }
            set { DomObj.Name = value; onPropertyChanged("Name"); }
        }
        public long Mobile
        {
            get { return DomObj.Mobile; }
            set { DomObj.Mobile =value; onPropertyChanged("Mobile"); }
        }

        public UserViewModel()
        {
            _UserList = new ObservableCollection<User>()
            {
                new User{UserID=1,Name="Sameera",Mobile=1122334455},
                new User{UserID=2,Name="aseer",Mobile=1234123455},
                new User{UserID=3,Name="nihal",Mobile=4545676789}
            };
            _addCommand = new RelayCommand(Add, CanAdd);
            _deleteCommand = new RelayCommand(Delete, CanDelete);
            _searchCommand = new RelayCommand(Search, CanSearch);
            _updateCommand = new RelayCommand(Update, CanUpdate);
        }
        public User SelectedUser
        {
            set
            {
                if (value != null)
                {
                    UserID = value.UserID;
                    Name = value.Name;
                    Mobile = value.Mobile;
                }
            }
        }
        #region RelayCommand
        private ICommand _addCommand;
        public ICommand AddUserCmd
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }
        public bool CanAdd(object obj)
        {
            //enable button only if mandatory fields are filled
            if (UserID != 0 && (Name != null)&& Mobile!=0)
            {
                return true;
            }
            return false;
        }
        public void Add(object obj)
        {
            //create a new local instance of user before adding dont use domobj since we use it for binding
            User userobj = new User();
            userobj.UserID = Convert.ToInt32(UserID);
            userobj.Name = Name;
            userobj.Mobile = Mobile;
            _UserList.Add(userobj);
            MessageBox.Show("User Added");
        }

        private ICommand _deleteCommand;
        public ICommand DeleteUserCmd
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }
        public bool CanDelete(object obj)
        {
            if (UserID != 0 && (_UserList.Count != 0))
            {
                return true;
            }
            return false;
        }
        public void Delete(object obj)
        {
            User obj1 = _UserList.FirstOrDefault(s => s.UserID == (int)obj);
            _UserList.Remove(obj1);
            MessageBox.Show("User deleted !");

        }

        private ICommand _searchCommand;
        public ICommand SearchUsercmd
        {
            get { return _searchCommand; }
            set { _searchCommand = value; }
        }
        public bool CanSearch(object obj)
        {
            if (UserID != 0)
            {
                return true;
            }
            return false;
        }
        public void Search(object obj)
        {
            User obj1 = _UserList.FirstOrDefault(s => s.UserID == (int)obj);
            UserID = obj1.UserID;
            Name = obj1.Name;
            Mobile = obj1.Mobile;

        }
        private ICommand _updateCommand;
        public ICommand UpdateUsercmd
        {
            get { return _updateCommand; }
            set { _updateCommand = value; }
        }
        public bool CanUpdate(Object obj)
        {
            if (UserID != 0)
            {
                return true;
            }
            return false;
        }
        public void Update(object obj)
        {
            User obj1 = _UserList.FirstOrDefault(s => s.UserID == (int)obj);
            obj1.UserID = UserID;
            obj1.Name = Name;
            obj1.Mobile = Mobile;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Validation
        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get
            {
                string result = string.Empty;
                if ( Mobile.ToString().Length >10 ||Mobile.ToString().Length<10)
                {
                    result = "Mobil should be 10 digits!";
                }
                return result;
            }
        }

        #endregion
    }
}
