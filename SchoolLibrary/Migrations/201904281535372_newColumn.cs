namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LostBooks", "LossDate", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LostBooks", "LossDate");
        }
    }
}
