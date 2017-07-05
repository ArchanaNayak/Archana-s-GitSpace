namespace UOA.ForumData.Analysis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotMappedForumKeyword : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ForumKeyWords", "KeyWord");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForumKeyWords", "KeyWord", c => c.String());
        }
    }
}
