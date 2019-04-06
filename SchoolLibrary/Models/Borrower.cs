

namespace SchoolLibrary.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    class Borrower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        public String FirstName { get;  set; }
        public String LastName { get; set; }
        public int IdentifierNumber { get; set; }
    }
}
