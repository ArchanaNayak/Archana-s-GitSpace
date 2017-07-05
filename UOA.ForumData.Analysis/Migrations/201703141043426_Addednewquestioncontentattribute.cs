namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewquestioncontentattribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "QuestionContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "QuestionContent");
        }
    }
}
