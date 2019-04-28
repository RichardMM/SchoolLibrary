

namespace SchoolLibrary
{
    using SchoolLibrary.Utilities;
    using System.Windows;
    using SchoolLibrary.Models;
    using System.Security.Cryptography;
    using SchoolLibrary.ViewModel;
    using System.Linq;
    using System.Windows.Controls;
    using SchoolLibrary.Pages;
    using System.Collections.Generic;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }


        public MainWindow()
        {

            InitializeComponent();
            DataContext = new AppViewModel();
        }

        public void NavigatePages(object sender, RoutedEventArgs e)
        {
            string senderName = ((MenuItem)sender).Name.ToString();
            switch (senderName)
            {
                case "HomeButton":
                    ViewModel.LoadBooks();
                    
                    ViewModel.CurrentPage = new ViewBooks(ViewModel);
                    break;
                case "AddBooksButton":
                    ViewModel.CurrentBook = new Book();
                    ViewModel.CurrentPage = new AddBook(ViewModel);
                    break;
                case "BorrowersButton":
                    ViewModel.CurrentPage = new Students(ViewModel);
                    break;
                case "BorrowedBooksButton":
                    ViewModel.CurrentPage = new BorrowedItemsView(ViewModel);
                    break;
                case "AdminButton":
                    ViewModel.CurrentPage = new AdminPage(ViewModel);
                    break;


            }
        }

        private void Startup(object sender, RoutedEventArgs e)
        {
            
            using (LibAppContext DbConn = new LibAppContext())
            {
                string password;


                using (MD5 md5Hash = MD5.Create())
                {
                    EncryptionTools obj = new EncryptionTools(md5Hash);
                    password = obj.GetMd5Hash("avertis");


                }
                
                int count = DbConn.Users.Count();
                if (count == 0) {
                    User Obj = new User(name: "admin", password: password);
                    DbConn.Users.Add(Obj);
                    DbConn.SaveChanges();
                }

                #region BorrowerTypes

                int currentTypes = DbConn.BorrowerTypes.Count();
                if (currentTypes == 0)
                {
                    //MessageBox.Show(currentTypes.ToString());
                    List<BorrowerType> defaults = new List<BorrowerType> { new BorrowerType() { TypeName = "Student" },
                                                                        new BorrowerType() { TypeName = "Teacher" }
                                                                        };
                    DbConn.BorrowerTypes.AddRange(defaults);
                    DbConn.SaveChanges();
                }
                #endregion

                DbConn.Dispose();

            }

        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            string name = ((MenuItem)sender).Name.ToString();
            ViewModel.LoadReport(name);
        }
    }
       
}
