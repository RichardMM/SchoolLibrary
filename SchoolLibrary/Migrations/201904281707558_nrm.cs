namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nrm : DbMigration
    {
        public override void Up()
        {
           // DropColumn("Borrowers", "RegistrationDate");
            DropColumn("LostBooks", "LossDate");
        }
        
        public override void Down()
        {
            AddColumn("LostBooks", "LossDate", c => c.DateTime(nullable: false, precision: 0));
           // AddColumn("Borrowers", "RegistrationDate", c => c.DateTime(nullable: false, precision: 0));
        }
    }
}
