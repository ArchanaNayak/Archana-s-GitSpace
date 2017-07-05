namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedquestiontextattribute : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "QuestionText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "QuestionText", c => c.String());
        }
    }
}
