using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Analysis.Helpers
{
    /* Performing initial setup*/
    public class Settings
    {        
        public string BaseDomainURL { get; set; }
        public string TargetWebURL { get; set; }
        public string RootCssName { get; set; }
        public string ForumCssName { get; set; }
        public int MaxForumsToCrawl { get; set; }
        public int MaxQuestionsToCrawl { get; set; }
        public Settings()
        {
            BaseDomainURL = ConfigurationManager.AppSettings["baseDomainUrl"];
            TargetWebURL = ConfigurationManager.AppSettings["targetWebUrl"];
            RootCssName = ConfigurationManager.AppSettings["rootWebCSSToParse"];
            ForumCssName = ConfigurationManager.AppSettings["forumWebCSSToParse"];
            MaxForumsToCrawl = Convert.ToInt32(ConfigurationManager.AppSettings["maxRootWebsToCrawl"]) == 0 ? 
                                                            int.MaxValue : 
                                                            Convert.ToInt32(ConfigurationManager.AppSettings["maxRootWebsToCrawl"]);
            MaxQuestionsToCrawl = Convert.ToInt32(ConfigurationManager.AppSettings["maxforumWebsToCrawl"]) == 0 ?
                                                            int.MaxValue :
                                                            Convert.ToInt32(ConfigurationManager.AppSettings["maxforumWebsToCrawl"]);
        }
    }
}