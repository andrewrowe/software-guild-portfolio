namespace TravelBlogCapstone.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNexFieldAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Age");
        }
    }
}
