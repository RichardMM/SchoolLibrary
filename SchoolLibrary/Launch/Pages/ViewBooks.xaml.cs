namespace SchoolLibrary.Pages
{

    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;

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
    }
}
