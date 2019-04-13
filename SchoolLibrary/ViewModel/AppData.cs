

namespace SchoolLibrary.ViewModel
{
    using SchoolLibrary.Models;
    using System.Linq;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using SchoolLibrary.Pages;
    public class AppViewModel : INotifyPropertyChanged
    {
        private string _userName;
        public string UserName {
            get
            {
                return _userName;
            }
            set {
                _userName = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility navBarVisibility;
        public Visibility NavBarVisibility {
            get
            {
                return navBarVisibility;
            }
            set
            {
                navBarVisibility = value;
                NotifyPropertyChanged();
            }
        } 

        private Page _currentContentPage;
        public Page CurrentPage {
            get
            {
                return _currentContentPage;
            }
            set
            {
                _currentContentPage = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public AppViewModel()
        {
            CurrentPage = new Login(this); ;
            navBarVisibility = Visibility.Hidden;
        }

        public bool VerifyPassword(string password)
        {
            using (LibAppContext conn = new LibAppContext())
            {
                var result = conn.Users.Where(user => user.Name == UserName).ToList();
                
                if (result == null || !result.Any())
                {
                    return false;
                }
                else
                {
                    return result.First<User>().ConfirmPassword(password);
                }

            }
        }
    }
}
