using System.Collections.Generic;
using System.Web.Mvc;
using UOA.ForumData.Analysis.DAL;
using UOA.ForumData.WebCrawler;
using UOA.ForumData.Core;
using UOA.ForumData.Analysis.Helpers;
using System.Configuration;
using System;

namespace UOA.ForumData.Analysis.Controllers
{
    public class CrawlerController : Controller
    {
        OnlineSupportForumDatabaseContext objcontext;
        public CrawlerController()
        {
            objcontext = new OnlineSupportForumDatabaseContext();
        }
        public ActionResult Index()
        {
            return View(new List<ForumDetail>());

        }
        public ActionResult BeginCrawl()
        {
            CleanUpBackendData();

            /* Prepare ConfigData */
            var _settings = new Settings();

            /* Start Web Crawl */
            var _crawler = new Crawler(_settings.BaseDomainURL,
                                       _settings.TargetWebURL,
                                       _settings.RootCssName,
                                       _settings.ForumCssName,
                                       _settings.MaxForumsToCrawl,
                                       _settings.MaxQuestionsToCrawl);

            List<ForumDetail> forums = _crawler.CrawlSite();

            /* Update the DB with Crawled Results */
            foreach (var forum in forums)
            {
                objcontext.ForumDetails.Add(forum);
            }
            objcontext.SaveChanges();
            return RedirectToAction("Index", "Analyser", forums);
        }

        /*Refresh the data on every web crawl*/
        private void CleanUpBackendData()
        {
            objcontext.Database.ExecuteSqlCommand("DELETE FROM Replies");
            objcontext.Database.ExecuteSqlCommand("DELETE FROM Questions");
            objcontext.Database.ExecuteSqlCommand("DELETE FROM ForumDetails");
        }
    }
}