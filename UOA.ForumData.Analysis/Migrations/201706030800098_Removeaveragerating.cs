namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeaveragerating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "AverageRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "AverageRating", c => c.Double(nullable: false));
        }
    }
}
