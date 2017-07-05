namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedquestionurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Url");
        }
    }
}
