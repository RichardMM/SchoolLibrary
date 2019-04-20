
namespace SchoolLibrary.Pages
{
    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
    using SchoolLibrary.Models;
    /// <summary>
    /// Interaction logic for Lend_Item.xaml
    /// </summary>
    public partial class Lend_Item : Page
    {
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }
        public Lend_Item(AppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void LendButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            switch (name)
            {
                case "CancelButton":
                    ViewModel.LendBook = new BorrowedItem();
                    ViewModel.CurrentPage = new ViewBooks(ViewModel);
                    break;
                case "SaveButton":
                    ViewModel.SaveNewBorrow();
                    break;
            }


        }
    }
}
