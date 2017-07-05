using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UOA.ForumData.WebCrawler.Extensions;
using UOA.ForumData.Core;

namespace UOA.ForumData.WebCrawler.Parsers
{
    /*Parses the Reply content*/
    public class ReplyTextParser
    {
        private List<Reply> _replies = new List<Reply>();
        public List<Reply> ParseText(CrawledPage page, string nodeSelectExpression)
        {
            HtmlDocument DocToParse = new HtmlDocument();
            DocToParse.LoadHtml(page.Text);
            HtmlNodeCollection nodes = DocToParse.DocumentNode.SelectNodes(nodeSelectExpression);
            if (nodes != null)
            {
                foreach (var node in nodes)
                {                    
                    Reply reply = new Reply();
                    reply.Text = GetReplyText(node);
                    reply.Date = GetReplyDate(node);
                    reply.RepliedBy = GetRepliedBy(node);
                    reply.IsaBestReply = CheckIfBestReply(node);
                    _replies.Add(reply);
                }
            }

            return _replies;
        }

        /* Checks for best reply */
        private bool CheckIfBestReply(HtmlNode node)
        {
            HtmlNode bestReplyNode = node.SelectSingleNode(".//div[@class='chosen']");
            return (bestReplyNode != null);
        }

        /* Gets the Reply Text */
        private string GetReplyText(HtmlNode node)
        {
            HtmlNode replyTextNode = node.SelectSingleNode(".//div[@class='answerContent']");
            if (replyTextNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(replyTextNode.InnerText);
        }

        /* Gets the Reply Date */
        private string GetReplyDate(HtmlNode node)
        {
            HtmlNode replyTextNode = node.SelectSingleNode(".//span[@class='date']");
            if (replyTextNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(replyTextNode.Attributes["title"].Value);
        }

        /* Gets the Author Details*/
        private string GetRepliedBy(HtmlNode node)
        {
            HtmlNode replyTextNode = node.SelectSingleNode(".//a[@class='profile']");
            if (replyTextNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(replyTextNode.InnerText);
        }
    }
}
