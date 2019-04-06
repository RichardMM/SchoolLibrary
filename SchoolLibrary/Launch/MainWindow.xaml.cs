using SchoolLibrary.Utilities;
using System.Windows;
using SchoolLibrary.Models;
using System.Security.Cryptography;
using SchoolLibrary.ViewModel;
using System.Linq;

namespace SchoolLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new AppViewModel();
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
