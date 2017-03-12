namespace Alga1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "AspNetUsersId", c => c.String());
            DropColumn("dbo.Employees", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "ApplicationUserId", c => c.String());
            DropColumn("dbo.Employees", "AspNetUsersId");
        }
    }
}
