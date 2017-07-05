using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UOA.ForumData.Analysis.DAL;

namespace UOA.ForumData.Analysis.Controllers
{
    /* Forum Data Controller */
    public class ForumDataController : Controller
    {
        OnlineSupportForumDatabaseContext objcontext;
        public ForumDataController()
        {
            objcontext = new OnlineSupportForumDatabaseContext();
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}