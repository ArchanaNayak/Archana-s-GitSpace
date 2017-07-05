using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UOA.ForumData.Core;

namespace UOA.ForumData.Analysis.DAL
{
    /* Data Access Layer */
    public class OnlineSupportForumDatabaseContext : DbContext
    {
        
            public OnlineSupportForumDatabaseContext()
                : base("name = OnlineSupportForumDatabaseConnection")
            {

            }

        public static OnlineSupportForumDatabaseContext Create()
        {
            return new OnlineSupportForumDatabaseContext();
        }

        public DbSet<CrawledPage> CrawledPages { get; set; }
            public DbSet<ForumDetail> ForumDetails { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Reply> Replies { get; set; }
            public DbSet<KeywordStatistic> KeyWordStatistics { get; set; }

        public System.Data.Entity.DbSet<UOA.ForumData.Core.ForumKeyWord> ForumKeyWords { get; set; }
    }
}