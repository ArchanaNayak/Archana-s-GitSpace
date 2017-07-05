namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedparameterstostatistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KeywordStatistics", "NumberOfQuestions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KeywordStatistics", "NumberOfQuestions");
        }
    }
}
