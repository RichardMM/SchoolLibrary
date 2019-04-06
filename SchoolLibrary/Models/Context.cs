
namespace SchoolLibrary.Models
{
    using System.Data.Entity;

    using MySql.Data.EntityFramework;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class LibAppContext:DbContext

    {
        public LibAppContext():base("name=LibAppContext")
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedItem> BorrowedItems { get; set; }




    }
}
