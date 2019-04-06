namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Design;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Migrations.Utilities;
    using System.Linq;

    class MyCodeGenerator : CSharpMigrationCodeGenerator
    {
        
        protected override void Generate(
            DropIndexOperation dropIndexOperation, IndentedTextWriter writer)
        {
            dropIndexOperation.Table = StripDbo(dropIndexOperation.Table);

            base.Generate(dropIndexOperation, writer);
        }

        // TODO: Override other Generate overloads that involve table names

        private string StripDbo(string table)
        {
            if (table.StartsWith("dbo."))
            {
                return table.Substring(4);
            }

            return table;
        }
    }

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolLibrary.Models.LibAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CodeGenerator = new MyCodeGenerator();
        }

        protected override void Seed(SchoolLibrary.Models.LibAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
