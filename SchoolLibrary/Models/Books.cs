using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary.Models
{
    public class Book
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string AccessionNumber { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string BookshelfNo { get; set; }
        public string RowNo { get; set; }
        public string ColumnPosition { get; set; }

        private DateTime lastUpdateDate;
        public DateTime LastUpdateDate
        {
            get
            {
                if (lastUpdateDate == DateTime.MinValue)
                {
                    lastUpdateDate = DateTime.Now;
                }
                return lastUpdateDate;
            }
            set
            {
                
                lastUpdateDate = value;
                
                
                 
            }
        }
        [NotMapped]
        public int Available { get; set; }
        public ICollection<BorrowedItem> BorrowedItems { get; set; }

        private int count;
        [Required]
        public int Count
        {
            get
            {
                return count;
            }
            set
            {

                count = value < 0 ? 0 : value;
            }
        }

        public Book()
        {
            BorrowedItems = new List<BorrowedItem>();
        }

    }
}
