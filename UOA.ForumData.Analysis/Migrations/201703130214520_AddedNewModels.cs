namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrawledPages",
                c => new
                    {
                        CrawledPageID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.CrawledPageID);
            
            CreateTable(
                "dbo.ForumDetails",
                c => new
                    {
                        ForumDetailID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ForumDetailID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.String(),
                        Author = c.String(),
                        AverageRating = c.Double(nullable: false),
                        ForumDetail_ForumDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.ForumDetails", t => t.ForumDetail_ForumDetailID)
                .Index(t => t.ForumDetail_ForumDetailID);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.String(),
                        RepliedBy = c.String(),
                        Rating = c.Double(nullable: false),
                        Question_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.ReplyID)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionID)
                .Index(t => t.Question_QuestionID);
           

        }

        public override void Down()
        {
            DropForeignKey("dbo.Questions", "ForumDetail_ForumDetailID", "dbo.ForumDetails");
            DropForeignKey("dbo.Replies", "Question_QuestionID", "dbo.Questions");
            DropIndex("dbo.Replies", new[] { "Question_QuestionID" });
            DropIndex("dbo.Questions", new[] { "ForumDetail_ForumDetailID" });
            DropTable("dbo.Replies");
            DropTable("dbo.Questions");
            DropTable("dbo.ForumDetails");
            DropTable("dbo.CrawledPages");
        }
    }
}
