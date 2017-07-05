namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removerating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Replies", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "Rating", c => c.Double(nullable: false));
        }
    }
}
