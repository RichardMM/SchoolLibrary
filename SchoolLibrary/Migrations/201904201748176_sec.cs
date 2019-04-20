namespace SchoolLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Users");
            AlterColumn("Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("Users", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("Users");
            AlterColumn("Users", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("Users", "Id");
        }
    }
}
