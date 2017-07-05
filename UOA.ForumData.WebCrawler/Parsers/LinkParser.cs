using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UOA.ForumData.Core;

namespace UOA.ForumData.WebCrawler.Parsers
{
    public class LinkParser
    {
       
       /*private instance fields*/
        private List<string> _urlList = new List<string>();        
      
        private const string _patternexpression = "href=\"[a-zA-Z./:&\\d_-]+\"";
        private List<string> _exceptions = new List<string>();
        public List<string> UrlList
        {
            get { return _urlList; }
            set { _urlList = value; }
        }   
        public List<string> Exceptions
        {
            get { return _exceptions; }
            set { _exceptions = value; }
        }
        public LinkParser() { }

        /*Parses a page looking for links*/
        public void ParseLinks(CrawledPage page, string sourceUrl, string nodeSelectExpression, int maxLinksToCrawl)
        {            
            HtmlDocument DocToParse = new HtmlDocument();
            DocToParse.LoadHtml(page.Text);
            HtmlNodeCollection nodes = DocToParse.DocumentNode.SelectNodes(nodeSelectExpression);
            if (nodes.Count > 0)
            {
                int max = nodes.Count > maxLinksToCrawl ? maxLinksToCrawl : nodes.Count;
                for (int n = 0; n < max; n++)
                {
                    string nodeInnerHTML = nodes[n].InnerHtml;
                    MatchCollection matches = Regex.Matches(nodeInnerHTML, _patternexpression);
                    for (int i = 0; i <= matches.Count - 1; i++)
                    {
                        Match url_Link = matches[i];
                        string hypertext_ref = null;
                        try
                        {
                            hypertext_ref = url_Link.Value.Replace("href=\"", "");
                            hypertext_ref = hypertext_ref.Substring(0, hypertext_ref.IndexOf("\""));
                        }
                        catch (Exception exc)
                        {
                            Exceptions.Add("Error while parsing the matched href: " + exc.Message);
                        }


                        if (!UrlList.Contains(hypertext_ref))
                        {                           
                                UrlList.Add(hypertext_ref);                            
                        }
                    }
                }
            }
        }
    }
}