namespace SchoolLibrary.Pages
{

    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
    using SchoolLibrary.Pages;
    using System.Windows;

    /// <summary>
    /// Interaction logic for VewBooks.xaml
    /// </summary>
    public partial class ViewBooks : Page
    {
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }
        public ViewBooks(AppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.CurrentPage = new AddBook(ViewModel);
        }

    }
}
