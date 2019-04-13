

namespace SchoolLibrary.Pages
{

    using System.Windows;
    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Page
    {
        
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }
        public AddBook(AppViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
        public void SaveBook(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveBookItem();
            
        }
    }
}
