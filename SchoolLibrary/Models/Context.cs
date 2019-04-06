using System.Data.Entity;

using MySql.Data.EntityFramework;

namespace SchoolLibrary.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class LibAppContext:DbContext

    {
        public LibAppContext():base("name=LibAppContext")
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }




    }
}
