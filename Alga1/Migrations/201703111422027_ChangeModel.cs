namespace Alga1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "User_Id" });
            AddColumn("dbo.Employees", "ApplicationUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "Employee_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Employee_Id");
            AddForeignKey("dbo.AspNetUsers", "Employee_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.AspNetUsers", new[] { "Employee_Id" });
            DropColumn("dbo.AspNetUsers", "Employee_Id");
            DropColumn("dbo.Employees", "ApplicationUserId");
            CreateIndex("dbo.Employees", "User_Id");
            AddForeignKey("dbo.Employees", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
