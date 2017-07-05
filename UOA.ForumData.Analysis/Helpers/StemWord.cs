using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Analysis.Helpers
{
    /* Noise words*/
    public class StemWord
    {
        public List<string> commonwords { get; set; }
        public StemWord()
        {
            commonwords = new List<string>();
            commonwords.Add("Thanks");
            commonwords.Add("Need");
            commonwords.Add("sorry");
            commonwords.Add("Regards");
            commonwords.Add("Error");
            commonwords.Add("Use");
            commonwords.Add("Code");
            commonwords.Add("Create");
            commonwords.Add("Trying");
            commonwords.Add("Same");
            commonwords.Add("See");
        }


    }
}