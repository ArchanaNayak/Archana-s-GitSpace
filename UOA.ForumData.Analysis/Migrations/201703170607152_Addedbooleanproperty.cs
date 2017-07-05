namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedbooleanproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Replies", "IsaBestReply", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Replies", "IsaBestReply");
        }
    }
}
