

namespace SchoolLibrary.ViewModel
{
    using SchoolLibrary.Models;
    using SchoolLibrary.Models.ReportModels;
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
    using System.Security.Cryptography;
    using SchoolLibrary.Utilities;
    using OfficeOpenXml;

    using System.IO;


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

        private string lossReason;
        public string LossReason
        {
            get
            {
                lossReason = lossReason == null ? "" : lossReason;
                return lossReason;
            }
            set
            {
                lossReason = value;
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
                item.Available = item.Count - item.BorrowedItems.Where(p => p.Returned==false).ToList().Count;
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
                //get current number of current books in DB
                int currentCount = 0;
                if (AppDbCxt.Entry(currentBook).State==EntityState.Modified && AppDbCxt.Entry(CurrentBook).GetDatabaseValues().ToObject() is Book clonedBook)
                {
                    currentCount = clonedBook.Count;


                }
                // check if number reduced
                bool reductionhappened = currentCount > CurrentBook.Count;
                if (reductionhappened && LossReason =="")
                {
                   
                    MessageBox.Show("You have reduced number of books and must provide a reason");
                    return;
                }

                //save if reduction happened
                if (reductionhappened)
                {
                    LostBook newLost = new LostBook { BookID = CurrentBook.Id, LossReason = LossReason };

                    AppDbCxt.LostBooks.Add(newLost);
                }
                 //DbConn.Entry(currentBook.BorrowedItems).State = DbConn.Entry(currentBook).State;
                 AppDbCxt.SaveChangesAsync();
                LossReason = "";
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
                if (CurrentBook.Available <= 0)
                {
                    MessageBox.Show("No Books are available to lend out");
                    CurrentPage = new ViewBooks(this);
                    return;
                }
                LendBook.BookId = CurrentBook.Id;
                dbConn.BorrowedItems.Add(LendBook);
                dbConn.SaveChanges();
               

            }
            LoadBorrowed();
            LoadBooks();
            CurrentPage = new ViewBooks(this);
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

        public void ChangeAdminDetails(string newPassword, string confirmPassword, string currentPassword)
        {
            string title = "Admin Details";
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match", title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User obj = AppDbCxt.Users.First(r => 1==1);
            // check if current password matches provided
            if(!obj.ConfirmPassword(currentPassword))
            {
                MessageBox.Show("Current Password Provided does not match stored password", title,MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            obj.Name = UserName;
            using (MD5 md5Hash = MD5.Create())
            {
                EncryptionTools enc = new EncryptionTools(md5Hash);
                obj.Password = enc.GetMd5Hash(newPassword);
            }

            AppDbCxt.SaveChanges();
            MessageBox.Show("Details Successfully changed", title, MessageBoxButton.OK, MessageBoxImage.Information);
            CurrentPage = new ViewBooks(this);
        }

        public void LoadReport(string ReportName)
        {
            LibAppContext conn = new LibAppContext();

            switch(ReportName)
            {
                case "Lost_Books":
                    List<LostItemsReport> res = (from item in conn.LostBooks
                               join books in conn.Books on item.BookID equals books.Id
                               select new LostItemsReport
                               {
                                   Id = item.Id,
                                   Title = books.Title,
                                   Author = books.Author,
                                   WriteOffDate = item.LossDate,
                                   Narration = item.LossReason

                               }
                               ).ToList();
                    ExportListUsingEPPlus<LostItemsReport>(res, namePrefix: "Write off Books");
                    break;
                case "Registered_Borrowers":
                    List<RegisteredBorrowers> borrowers = (from row in conn.Borrowers
                                                           join names in conn.BorrowerTypes on row.TypeName_Id equals names.Id
                                                           select new RegisteredBorrowers
                                                           {
                                                               Id = row.IdentificationNumber,
                                                               Name = row.FirstName + " " + row.LastName,
                                                               Email = row.EmailAddress,
                                                               Type = names.TypeName
                                                           }).ToList();
                    ExportListUsingEPPlus(borrowers, "Registered Borrowers");
                    break;
                case "Borrowed_Books":
                    ExportListUsingEPPlus(BorrowedBooksView,  "BorrowedBooks");
                    break;

                default:
                    ExportListUsingEPPlus(LibraryBooks, "All Books");
                    break;
               
            }
            conn.Dispose();
        }

        public void ExportListUsingEPPlus<T>(List<T> data, string namePrefix)
        {
            if (!data.Any())
            {
                return;
            }
            //create avertis Library directory
            var di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Avertis" + Path.DirectorySeparatorChar + "Library");
            if (!di.Exists)
            {
                di.Create();
            }
            //create file path
            string filePath = di.FullName + Path.DirectorySeparatorChar + namePrefix + DateTime.Now.ToString("_yyyy_MM_dd") + ".xlsx";

            var fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                fi.Delete();  
               
            }

            //write excel

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Report");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            excel.SaveAs(fi);
            System.Diagnostics.Process.Start(filePath);

        }
    }
}
