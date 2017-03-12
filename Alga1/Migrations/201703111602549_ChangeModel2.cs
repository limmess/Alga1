namespace Alga1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeModel2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "AspNetUsersId");
        }

        public override void Down()
        {
            AddColumn("dbo.Employees", "AspNetUsersId", c => c.String());
        }
    }
}
