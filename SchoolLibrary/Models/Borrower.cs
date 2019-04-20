

namespace SchoolLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Borrower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }

        [NotMapped]
        public String BothNames
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }
        public String EmailAddress { get; set; }
        public int TypeName_Id { get; set; }
        public virtual BorrowerType TypeName { get; set; }
        public string IdentificationNumber { get; set; }
    }

    public class BorrowerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TypeName {get; set;}
        public virtual ICollection<Borrower> Borrowers { get; set; }

    }
}
