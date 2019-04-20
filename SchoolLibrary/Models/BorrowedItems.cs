
namespace SchoolLibrary.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// User only needs to set the borrowerId, BookId and NoOfDays
    /// </summary>
    public class BorrowedItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private int borrowerId;
        public int BorrowerId 
        {
            get
            {
                return borrowerId;
            }
            set
            {
                borrowerId = value;
                if(BorrowDate == null || System.DateTime.Compare(BorrowDate,System.DateTime.MinValue)==0)
                {
                    BorrowDate = System.DateTime.Now;
                }
                
            }
        }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }
        /// <summary>
        /// Default Value is False
        /// </summary>
        public bool Returned { get; set; }
        
        private int noOfDays;
        [NotMapped]
        public int NoOfDays
        {
            get
            {
                return noOfDays;
            }
            set
            {
                noOfDays = value;
                ReturnDate = BorrowDate.AddDays(noOfDays);
            }
        }

        public override String ToString()
        {
            string res = String.Format("BorrowerId- {0} BookId- {1}\n BorrowDate- {2} ReturnDate- {3}\n Returned- {4} NoOfDays {5}",
                                        BorrowerId, BookId, BorrowDate, ReturnDate, Returned, NoOfDays);
            return res;
        }
    }
}
