


namespace SchoolLibrary.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }
        public Login(AppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void PerformLogin(object sender, RoutedEventArgs e)
        {
            
           
            if (ViewModel.VerifyPassword(PasswordInput.Password)){
                //ViewModel.CurrentPage = new AddBook(viewModel: ViewModel);
                ViewModel.NavBarVisibility = Visibility.Visible;
                ViewModel.CurrentPage = new ViewBooks(viewModel: ViewModel);
            }
            else
            {
                MessageBox.Show("Incorrect Password");
            }
        }
    }
}
