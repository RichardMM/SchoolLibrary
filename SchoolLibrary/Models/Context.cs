using System.Data.Entity;
using System.Data.SQLite;

namespace SchoolLibrary.Models
{
    class LibAppContext:DbContext

    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
