namespace Alga1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4615ffe7-3fb6-4756-9c50-5fb9fb5e7cc6', N'admin@alga.com', 0, N'AENTubsQXEUKzS51sGKfwS3H6OJdQCyPi0dUaocICjZJp087DmVSmx8mTXNLK5C5lQ==', N'55b92705-3023-452c-b343-e9e0310de936', NULL, 0, 0, NULL, 1, 0, N'admin@alga.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'655c922d-2c69-4a37-8650-6684ecc0cb03', N'guest@alga.com', 0, N'AOeopKlZHbkQ/11osrjXuj49+Cst0V9ExndYMTEtq83sfI2Jp9iMEdJ5zynVZ4W/Wg==', N'6c1ef8bb-c291-4348-aa11-632220941355', NULL, 0, 0, NULL, 1, 0, N'guest@alga.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0fbb0a70-2def-4aca-8801-f76b9fc0ff5d', N'Admin')


INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4615ffe7-3fb6-4756-9c50-5fb9fb5e7cc6', N'0fbb0a70-2def-4aca-8801-f76b9fc0ff5d')

");
        }

        public override void Down()
        {
        }
    }
}
