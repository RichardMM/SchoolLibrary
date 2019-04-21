
namespace SchoolLibrary.Models
{
    using System.Data.Entity;

    using MySql.Data.EntityFramework;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LibAppContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedItem> BorrowedItems { get; set; }
        public DbSet<BorrowerType> BorrowerTypes { get; set; }
        public DbSet<LostBook> LostBooks { get; set; }
        public LibAppContext() : base("name=LibAppContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Borrower>()
                .HasRequired<BorrowerType>(s => s.TypeName)
                .WithMany(g => g.Borrowers)
                .HasForeignKey<int>(s => s.TypeName_Id);

            modelBuilder.Entity<BorrowedItem>()
                .HasRequired<Book>(s => s.Book)
                .WithMany(g => g.BorrowedItems)
                .HasForeignKey<int>(s => s.BookId);
        }
       
    }


}   



