namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifiedquestioncontentattribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Content", c => c.String());
            DropColumn("dbo.Questions", "QuestionContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "QuestionContent", c => c.String());
            DropColumn("dbo.Questions", "Content");
        }
    }
}
