namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RempvedquestiosfromForum : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumKeyWords",
                c => new
                    {
                        ForumKeywordID = c.Int(nullable: false, identity: true),
                        KeyWord = c.String(),
                    })
                .PrimaryKey(t => t.ForumKeywordID);
            
            AddColumn("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID", c => c.Int());
            CreateIndex("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID");
            AddForeignKey("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID", "dbo.ForumKeyWords", "ForumKeywordID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID", "dbo.ForumKeyWords");
            DropIndex("dbo.ForumDetails", new[] { "ForumKeyWord_ForumKeywordID" });
            DropColumn("dbo.ForumDetails", "ForumKeyWord_ForumKeywordID");
            DropTable("dbo.ForumKeyWords");
        }
    }
}
