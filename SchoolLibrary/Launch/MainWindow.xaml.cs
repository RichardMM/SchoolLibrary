

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
                    ViewModel.CurrentPage = new ViewBooks(ViewModel);
                    break;
                case "AddBooksButton":
                    ViewModel.CurrentPage = new AddBook(ViewModel);
                    break;
                case "StudentsButton":
                    //ViewModel.CurrentPage = new ViewBooks(ViewModel);
                    break;
                case "BorrowedBooksButton":
                    //ViewModel.CurrentPage = new ViewBooks(ViewModel);
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
                    password = obj.GetMd5Hash("123456");


                }



                
                int count = DbConn.Users.Count();
                if (count == 0) {
                    User Obj = new User(name: "admin", password: password);
                    DbConn.Users.Add(Obj);
                    DbConn.SaveChanges();
                }
  

                //MessageBox.Show(DbConn.Users.Find(1).ConfirmPassword("123456").ToString());
                DbConn.Dispose();

            }

        }

    }
       
}
