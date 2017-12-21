namespace CarDealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedToUsertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RoleName");
        }
    }
}
