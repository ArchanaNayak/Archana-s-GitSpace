using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace UOA.ForumData.WebCrawler.Extensions
{
    public static class StringExtension
    {
        /* Removing whitespace from the crawled data (html context) */
        public static string RemoveWhiteSpaceCharacters(string str)
        {            
            string trimmedspace = str.Trim();
            return HttpUtility.HtmlDecode(trimmedspace);
        }
    }
}