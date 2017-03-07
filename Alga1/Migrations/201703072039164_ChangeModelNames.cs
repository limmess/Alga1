namespace Alga1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Employees", "Surname", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Employees", "SalaryNet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Employees", "ChildrenNo", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "RaisesChildrenAlone", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "EmployeePhoto", c => c.Binary());
            DropColumn("dbo.Employees", "Vardas");
            DropColumn("dbo.Employees", "Pavarde");
            DropColumn("dbo.Employees", "AlgaNet");
            DropColumn("dbo.Employees", "VaikuSkaicius");
            DropColumn("dbo.Employees", "AuginaVaikusVienas");
            DropColumn("dbo.Employees", "AsmuoImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "AsmuoImage", c => c.Binary());
            AddColumn("dbo.Employees", "AuginaVaikusVienas", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "VaikuSkaicius", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "AlgaNet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Employees", "Pavarde", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Employees", "Vardas", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Employees", "EmployeePhoto");
            DropColumn("dbo.Employees", "RaisesChildrenAlone");
            DropColumn("dbo.Employees", "ChildrenNo");
            DropColumn("dbo.Employees", "SalaryNet");
            DropColumn("dbo.Employees", "Surname");
            DropColumn("dbo.Employees", "Name");
        }
    }
}
