

namespace SchoolLibrary.Pages
{
    using SchoolLibrary.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }

        public AdminPage(AppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.ChangeAdminDetails(NewPasswordBox.Password, ConfirmBox.Password, CurrentPasswordBox.Password);
        }
    }
}
