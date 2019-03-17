using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary.Models
{
    class Book
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string BookshelfNo { get; set; }
        public string RowNo { get; set; }
        public string ColumnPosition { get; set; } 
        public int Count { get; set; }
        public DateTime LastUpdateDate { get; set; }

    }
}
