namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForumKeyword : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID", "dbo.ForumKeyWords");
            DropIndex("dbo.ForumDetails", new[] { "ForumKeyWord_ForumKeywordID" });
            DropColumn("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID", c => c.Int());
            CreateIndex("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID");
            AddForeignKey("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID", "dbo.ForumKeyWords", "ForumKeywordID");
        }
    }
}
