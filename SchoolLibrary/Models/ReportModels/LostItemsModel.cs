
namespace SchoolLibrary.Models.ReportModels
{

    using System;

    public class LostItemsReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime WriteOffDate { get; set; }
        public string Author { get; set; }
        public string Narration { get; set; }
    }
}
