
using System.Windows.Controls;
using SchoolLibrary.ViewModel;

namespace SchoolLibrary.Pages
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Page
    {
        public AppViewModel ViewModel { get; set; }
        public AddBook(AppViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
    }
}
