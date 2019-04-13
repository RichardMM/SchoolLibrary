using System;

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
        [Required]
        public int Count { get; set; }
        public DateTime LastUpdateDate { get; set; }

    }
}
