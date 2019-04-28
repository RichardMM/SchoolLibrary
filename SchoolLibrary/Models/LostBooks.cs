

namespace SchoolLibrary.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    public class LostBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookID { get; set; }
        public string LossReason { get; set; }

        private DateTime lossDate;
        [NotMapped]
        public DateTime LossDate
        {
            get
            {
                if (lossDate == DateTime.MinValue)
                {
                    lossDate = DateTime.Now;
                }
                    return lossDate;
            }
            set
            {
                
                    lossDate = value;
                
                 
            }
        }

    }
}
