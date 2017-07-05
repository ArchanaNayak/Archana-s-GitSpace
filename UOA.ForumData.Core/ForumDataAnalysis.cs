using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    public class ForumDataAnalysis
    {
        public ForumDataAnalysis()
        {
            forums = new List<ForumDetail>();
        }       
        public decimal Rank { get; set; }
        public List<ForumDetail> forums { get; set; }
    }
}