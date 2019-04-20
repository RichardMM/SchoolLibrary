
namespace SchoolLibrary.Pages
{
    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
    /// <summary>
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }

        public Students(AppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Save_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.SaveRegisteredBorrowerDetails();
        }

        private void Students_DataGrid_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
            RegisteredBorrowerView.CancelEdit(DataGridEditingUnit.Row);
        }
    }
}
