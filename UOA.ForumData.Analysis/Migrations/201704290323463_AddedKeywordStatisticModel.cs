namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedKeywordStatisticModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeywordStatistics",
                c => new
                    {
                        KeywordStatisticID = c.Int(nullable: false, identity: true),
                        KeyWord = c.String(),
                        ForumName = c.String(),
                        ForumUrl = c.String(),
                        QuestionName = c.String(),
                        QuestionUrl = c.String(),
                        KeywordOccurences = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KeywordStatisticID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KeywordStatistics");
        }
    }
}
