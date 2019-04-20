namespace SchoolLibrary.Pages
{

    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentBook?.Id == null || ViewModel.CurrentBook?.Id == 0)
            {
                MessageBox.Show("You Must select a book to be edit detail");
            }
            else
            {
                ViewModel.CurrentPage = new AddBook(ViewModel);
            }
            
        }

        private void LendButton(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentBook?.Id == null || ViewModel.CurrentBook?.Id ==0)
            {
                MessageBox.Show("You Must select a book to be lent out");
            }
            else
            {
                ViewModel.CurrentPage = new Lend_Item(ViewModel);
            }
            
        }
    }
}
