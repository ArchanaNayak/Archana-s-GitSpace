using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using UOA.ForumData.Core;
using UOA.ForumData.WebCrawler.Extensions;

namespace UOA.ForumData.WebCrawler.Parsers
{
    public class ForumTextParser
    {
        /* Parses the contents of the forum */
        public ForumDetail ParseText(CrawledPage page, string nodeSelectExpression)
        {
            HtmlDocument DocToParse = new HtmlDocument();
            DocToParse.LoadHtml(page.Text);
            HtmlNode node = DocToParse.DocumentNode.SelectSingleNode(nodeSelectExpression);
            if (node == null) return null;
            ForumDetail forum = new ForumDetail();
            forum.Url = page.Url;
            forum.Name = StringExtension.RemoveWhiteSpaceCharacters(node.InnerText);
            return forum;
        }

        /* Processes multiple pages inside the forum */
        public bool HasMultiplePages(CrawledPage page, string nodeSelectExpression)
        {
            HtmlDocument DocToParse = new HtmlDocument();
            DocToParse.LoadHtml(page.Text);
            HtmlNode node = DocToParse.DocumentNode.SelectSingleNode(nodeSelectExpression);
            return node != null;
        }

        /* Checks all the pages untill the end page*/
        public string ParseNextPageLink(CrawledPage page, string nodeSelectExpression)
        {
            HtmlDocument DocToParse = new HtmlDocument();
            DocToParse.LoadHtml(page.Text);
            HtmlNode node = DocToParse.DocumentNode.SelectSingleNode(nodeSelectExpression);
            return node.Attributes["href"].Value;
        }
    }
}