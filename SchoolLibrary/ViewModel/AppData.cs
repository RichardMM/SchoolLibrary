

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


        public BorrowedItem LendBook { get; set; } = new BorrowedItem();

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

        public List<ViewBorrowTableItems> BorrowedBooksView { get; set; }

        public AppViewModel()
        {
            CurrentPage = new Login(this); ;
            navBarVisibility = Visibility.Hidden;
            currentBook = new Book();
            //loading books is done ina custome manner
            LoadBooks();

            registeredBorrowers = AppDbCxt.Borrowers.ToList();
            LoadBorrowed();
        }

        public void LoadBooks()
        {
            LibraryBooks = AppDbCxt.Books.Include(p => p.BorrowedItems).ToList();
            foreach (var item in LibraryBooks)
            {
                //MessageBox.Show(item.BorrowedItems.ToList().Count.ToString());
                item.Available = item.Count - item.BorrowedItems.ToList().Count;
            }
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
            
             try
             {
                 AppDbCxt.Books.Add(CurrentBook);
                 AppDbCxt.Entry(currentBook).State = currentBook.Id == 0 ? EntityState.Added : EntityState.Modified;
                 //DbConn.Entry(currentBook.BorrowedItems).State = DbConn.Entry(currentBook).State;
                 AppDbCxt.SaveChangesAsync();

                 MessageBox.Show(String.Format("{1} was Saved successfully \n Author - {0}", CurrentBook.Author, currentBook.Title));
                 CurrentBook = new Book();
             }
             catch (DbEntityValidationException)
             {

                 MessageBox.Show("Mandatory Book Information is missing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        public void SaveNewBorrow()
        {
            using(LibAppContext dbConn= new LibAppContext())
            {
                LendBook.BookId = CurrentBook.Id;
                dbConn.BorrowedItems.Add(LendBook);
                dbConn.SaveChanges();

            }
            LoadBorrowed();
        }
        public void SaveBorrowedItemsState()
        {
            using (LibAppContext dbConn = new LibAppContext())
            {
                foreach(ViewBorrowTableItems row in BorrowedBooksView)
                {
                   BorrowedItem singleItem =  dbConn.BorrowedItems.First(el => el.Id == row.BorrowedId);
                    
                    singleItem.Returned = row.Returned;
                   
                    dbConn.SaveChanges();
                }
                
            }
        }
        public void LoadBorrowed()
        {
            using(LibAppContext conn = new LibAppContext())
            {
                BorrowedBooksView = (from bitem in conn.BorrowedItems
                                     join user in conn.Borrowers on bitem.BorrowerId equals user.Id
                                     join books in conn.Books on bitem.BookId equals books.Id
                                     select new ViewBorrowTableItems
                                     {
                                         BorrowedId = bitem.Id,
                                         User = user,
                                         BorrowedBookName = books.Title,
                                         BorrowDate = bitem.BorrowDate,
                                         DueDate = bitem.ReturnDate,
                                         Returned = bitem.Returned


                                     }).ToList();
            }
        }
    }
}
