using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UOA.ForumData.WebCrawler.Extensions;
using UOA.ForumData.Core;

namespace UOA.ForumData.WebCrawler.Parsers
{
    /*Parses the question content*/
    public class QuestionTextParser
    {
        public Question ParseText(CrawledPage page, string nodeSelectExpression)
        {
            HtmlDocument DocToParse = new HtmlDocument();
            DocToParse.LoadHtml(page.Text);
            HtmlNode node = DocToParse.DocumentNode.SelectSingleNode(nodeSelectExpression);
            if (node == null) return null;
            Question question = new Question();
            question.Text = GetQuestionText(node);
            question.Author = GetQuestionAuthor(node);
            question.Date = GetQuestionPostedDate(node);
            question.Content = GetQuestionContent(node);
            question.Url = page.Url;
            return question;
        }

        /* Gets the Question title text */
        private string GetQuestionText(HtmlNode node)
        {
            HtmlNode questionTitleNode = node.SelectSingleNode(".//h2[@id='currentTitle']");
            if (questionTitleNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(questionTitleNode.InnerText);
        }

        /* Gets the Date when the question was posted */
        private string GetQuestionPostedDate(HtmlNode node)
        {
            HtmlNode questionDateNode = node.SelectSingleNode(".//span[@class='date']");
            if (questionDateNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(questionDateNode.Attributes["title"].Value);
        }

        /* Gets the details of the author posting the question */
        private string GetQuestionAuthor(HtmlNode node)
        {
            HtmlNode questionAuthorNode = node.SelectSingleNode(".//a[@class='profile']");
            if (questionAuthorNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(questionAuthorNode.InnerText);
        }

        /* Gets the detailed question content */
        private string GetQuestionContent(HtmlNode node)
        {
            HtmlNode questionAuthorNode = node.SelectSingleNode(".//div[@class='currentDetails']");
            if (questionAuthorNode == null) return string.Empty;
            return StringExtension.RemoveWhiteSpaceCharacters(questionAuthorNode.InnerText);
        }

        
    }
}