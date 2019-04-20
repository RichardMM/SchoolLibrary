
namespace SchoolLibrary.ViewModel
{
    using System;
    using SchoolLibrary.Models;
    public class ViewBorrowTableItems
    {
        public int BorrowedId { get; set; }
        public Borrower User { get; set; }
        public string BorrowerName
        {
            get
            {
                return User.BothNames;
            }
        }
        public string BorrowedBookName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }
        public int DaysLent
        {
            get
            {
                return DateTime.Now.Subtract(BorrowDate).Days;
            }
        }



    }
}
