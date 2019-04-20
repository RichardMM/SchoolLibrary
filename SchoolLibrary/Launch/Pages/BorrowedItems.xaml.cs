

namespace SchoolLibrary.Pages
{
    using System.Windows.Controls;
    using SchoolLibrary.ViewModel;
    /// <summary>
    /// Interaction logic for BorrowedItems.xaml
    /// </summary>
    public partial class BorrowedItemsView : Page
    {
        public AppViewModel ViewModel
        {
            get
            {
                return DataContext as AppViewModel;
            }
        }
        public BorrowedItemsView(AppViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            BorrowViewGrid.CancelEdit(DataGridEditingUnit.Row);
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.SaveBorrowedItemsState();
        }
    }
}
