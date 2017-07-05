using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UOA.ForumData.Core;
using UOA.ForumData.WebCrawler.Parsers;

namespace UOA.ForumData.WebCrawler
{
    public class Crawler

    {
        /*Initialising*/
        private static List<CrawledPage> _pages = new List<CrawledPage>();
        private static List<ForumDetail> _forums = new List<ForumDetail>();
        private static string _baseDomainURL = string.Empty;
        private static string _targetWebURL = string.Empty;
        private static string _rootCssToGet = string.Empty;
        private static string _forumCssToGet = string.Empty;
        private static int _maxRootWebCount = 0;
        private static int _maxforumWebCount = 0;
        private static int _pageCount = 0;
        private static List<string> _unacceptableurls = new List<string>();


        public Crawler(string baseDomainURL,string targetWebURL, 
                                            string rootCssToGet,string forumCssToGet, 
                                            int maxRootWebCount,int maxForumWebCount)
        {

            _baseDomainURL = baseDomainURL;
            _targetWebURL = targetWebURL;
            _rootCssToGet = rootCssToGet;
            _forumCssToGet = forumCssToGet;
            _maxRootWebCount = maxRootWebCount;
            _maxforumWebCount = maxForumWebCount;            
        }

        /*Gets the response text for the url given*/
        private string GetWebText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "A .NET Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
        }

        /* Begins crawling the site*/
        public List<ForumDetail> CrawlSite()
        {
            _forums = new List<ForumDetail>();           
            CrawlMainPage(_baseDomainURL, _targetWebURL, _rootCssToGet, _maxRootWebCount);
            return _forums;
        }

       

        
        /* Crawls the main page */
        private void CrawlMainPage(string baseDomainURL, string targetURL, string classToGet, int maxLinksToCrawl)
        {
            string htmlText = GetWebText(targetURL);
            CrawledPage page = new CrawledPage();
            page.Text = htmlText;
            page.Url = targetURL;

            _pages.Add(page);
            LinkParser linkParser = new LinkParser();
            string nodeSelectExpression = "//td[@class='" + classToGet + "']";
            linkParser.ParseLinks(page, baseDomainURL, nodeSelectExpression, maxLinksToCrawl);

            foreach (string link in linkParser.UrlList)
            {
                _pageCount = 0;
                string validlink = link;
                try
                {
                    validlink = GeneratePath(baseDomainURL, validlink);
                    if (validlink != String.Empty)
                    {
                        ForumDetail forum = CrawlForumPage(baseDomainURL, validlink, _forumCssToGet, _maxforumWebCount, null);
                        _forums.Add(forum);
                    }

                }
                catch (Exception exc)
                {
                    _unacceptableurls.Add(validlink + " (page at url " + targetURL + ") - " + exc.Message);

                }
            }
        }

        /*Crawls a web page*/  
        private ForumDetail CrawlForumPage(string baseDomainURL, string targetURL, string classToGet, int maxLinksToCrawl, ForumDetail forum)
        {
            string htmlText = GetWebText(targetURL);
            CrawledPage page = new CrawledPage();
            page.Text = htmlText;
            page.Url = targetURL;
            _pages.Add(page);

            ForumTextParser forumParser = new ForumTextParser();
            string nodeSelectExpression = "//div[@class='main']/h1";
            if (forum == null) forum = forumParser.ParseText(page, nodeSelectExpression);

            LinkParser linkParser = new LinkParser();
            nodeSelectExpression = "//div[@class='" + classToGet + " ']";
            linkParser.ParseLinks(page, baseDomainURL, nodeSelectExpression, maxLinksToCrawl);
            _pageCount += linkParser.UrlList.Count;
            if (_pageCount <= _maxforumWebCount)
            {
                foreach (string link in linkParser.UrlList)
                {
                    string formattedLink = link;
                    try
                    {
                        formattedLink = GeneratePath(baseDomainURL, formattedLink);
                        if (formattedLink != String.Empty)
                        {
                            Question question = CrawlIndividualPage(baseDomainURL, formattedLink, _forumCssToGet, _maxforumWebCount);
                            if (question != null) forum.Questions.Add(question);
                        }
                    }
                    catch (Exception exc)
                    {
                        _unacceptableurls.Add(formattedLink + " (on page at url " + targetURL + ") - " + exc.Message);
                    }
                }
                nodeSelectExpression = "//div[@class='topicPages']//a[@class='right']";
                bool hasMultiPages = forumParser.HasMultiplePages(page, nodeSelectExpression);
                if (hasMultiPages)
                {
                    string nextPageLink = forumParser.ParseNextPageLink(page, nodeSelectExpression);
                    nextPageLink = GeneratePath(baseDomainURL, nextPageLink);
                    forum = CrawlForumPage(baseDomainURL, nextPageLink, _forumCssToGet, _maxforumWebCount, forum);
                }
            }
            return forum;
        }

        /*Crawls Individual page*/
        private Question CrawlIndividualPage(string baseDomainURL, string targetURL, string classToGet, int maxLinksToCrawl)
        {
            string htmlText = GetWebText(targetURL);
            CrawledPage page = new CrawledPage();
            page.Text = htmlText;
            page.Url = targetURL;
            _pages.Add(page);

            Question question = CrawlQuestions(page);
            List<Reply> replies = CrawlReplies(page);
            if (replies.Count > 0) question.Replies = replies;

            return question;
        }



        private Question CrawlQuestions(CrawledPage page)
        {
            string nodeSelectExpression = "//div[@class='questionContentWrapper']";
            QuestionTextParser questionParser = new QuestionTextParser();
            return questionParser.ParseText(page, nodeSelectExpression);

        }
        private List<Reply> CrawlReplies(CrawledPage page)
        {
            string nodeSelectExpression = "//div[contains(@class, 'answer   ') or contains(@class, 'answer   by-staff') or contains(@class, 'answer  best by-manager')]";
            ReplyTextParser replyParser = new ReplyTextParser();
            return replyParser.ParseText(page, nodeSelectExpression);
        }


        /*Generates an absolute path*/
        public string GeneratePath(string originatingUrl, string link)
        {
            return originatingUrl + link;
        }


        
        /* Examines whether the page has been crawled*/
        private static bool PageHasBeenCrawled(string url)
        {
            foreach (CrawledPage page in _pages)
            {
                if (page.Url == url)
                    return true;
            }

            return false;
        }


       
    }
}
