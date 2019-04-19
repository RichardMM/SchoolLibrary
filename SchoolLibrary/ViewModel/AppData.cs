

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
    using System.Data.Entity.Validation;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;

    public class AppViewModel : INotifyPropertyChanged
    {
        

        public LibAppContext AppDbCxt = new LibAppContext();


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

        private List<Book> libraryBooks;
        public List<Book> LibraryBooks
        {
            get
            {
                return libraryBooks;
            }
            set
            {
                libraryBooks = value;
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


        private Book currentBook;
        public Book CurrentBook
        {
            get
            {
   
                return currentBook;
            }
            set
            {
                currentBook = value;
                NotifyPropertyChanged();
            }
        }

        private List<BorrowerType> possibleUserTypes;

        public List<BorrowerType> PossibleUserTypes
        {
            get
            {
                if (possibleUserTypes == null)
                {
                    
                    
                        possibleUserTypes = AppDbCxt.BorrowerTypes.ToList();
                    
                }

                return possibleUserTypes;
            }

        }


        private List<Borrower> registeredBorrowers;
        public List<Borrower> RegisteredBorrowers
        {
            get
            {
              
                
                return registeredBorrowers;
            }
            set { registeredBorrowers = value;  NotifyPropertyChanged(); }
        }

        public AppViewModel()
        {
            CurrentPage = new Login(this); ;
            navBarVisibility = Visibility.Hidden;
            currentBook = new Book();
            using (LibAppContext conn = new LibAppContext())
            {
                LibraryBooks = conn.Books.ToList();
                
            }
            registeredBorrowers = AppDbCxt.Borrowers.ToList();

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
        public void SaveBookItem()
        {
            using (LibAppContext DbConn = new LibAppContext())
            {

                try
                {
                    DbConn.Books.Add(CurrentBook);
                    DbConn.Entry(currentBook).State = currentBook.Id == 0 ? EntityState.Added : EntityState.Modified;
                    DbConn.SaveChangesAsync();

                    MessageBox.Show(String.Format("{1} was Saved successfully \n Author - {0}", CurrentBook.Author, currentBook.Title));
                    CurrentBook = new Book();
                }
                catch (DbEntityValidationException)
                {

                    MessageBox.Show("Mandatory Book Information is missing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        public void SaveRegisteredBorrowerDetails()
        {
            
            
                foreach(Borrower el in RegisteredBorrowers)
                {
               
                AppDbCxt.Borrowers.Add(el);
                AppDbCxt.Entry(el).State = el.Id == 0 ? EntityState.Added : EntityState.Modified;
                AppDbCxt.SaveChanges();
                }
                
                
            
        }
    }
}
