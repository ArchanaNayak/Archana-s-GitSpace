using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    public class KeywordStatistic
    {
        public int KeywordStatisticID { get; set; }
        public string KeyWord { get; set; }
        public string ForumName { get; set; }
        public string ForumUrl { get; set; }
        public string QuestionName { get; set; }
        public string QuestionUrl { get; set; }
        public int KeywordOccurences { get; set; }       
        public int NumberOfQuestions { get; set; }
        [NotMapped]
        public int NumberOfForums { get; set; }
    }
}