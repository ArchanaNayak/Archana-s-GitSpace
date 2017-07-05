using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UOA.ForumData.Analysis.DAL;
using UOA.ForumData.Core;
using UOA.ForumData.WebCrawler;


namespace UOA.ForumData.Analysis.Controllers
{
    public class AnalyserController : Controller
    {

        OnlineSupportForumDatabaseContext objcontext;
        public AnalyserController()
        {
            objcontext = new OnlineSupportForumDatabaseContext();
        }
        /* Access the Forum Details */
        public ActionResult Index()
        {
            var forums = objcontext.ForumDetails
                         .Include(x => x.Questions.Select(y => y.Replies))
                         .ToList();
            return View(forums);
        }
    }
}