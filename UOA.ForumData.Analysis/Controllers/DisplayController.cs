using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using UOA.ForumData.Analysis.DAL;
using UOA.ForumData.Core;
using UOA.ForumData.WebCrawler;
using UOA.ForumData.TopicAnalyser;
using UOA.ForumData.Analysis.Helpers;
using System.Configuration;

namespace UOA.ForumData.Analysis.Controllers
{
    public class DisplayController : Controller
    {
        OnlineSupportForumDatabaseContext objcontext;


        public DisplayController()
        {
            objcontext = new OnlineSupportForumDatabaseContext();

        }
        public ActionResult Index()

        {
            return View();
        }

        /* Computes and selects maximum number of keywords to be displayed*/
        public ActionResult KeywordBasedContent()

        {
            CleanUpBackendData();
            var forums = objcontext.ForumDetails
                         .Include(x => x.Questions.Select(y => y.Replies))
                         .ToList();
            int maxKeyWordsToDisplay;
            bool isSuccess = int.TryParse(ConfigurationManager.AppSettings["maxKeyWordsToDisplay"], 
                                                                          out maxKeyWordsToDisplay);
            if (!isSuccess) maxKeyWordsToDisplay = int.MaxValue;

            int maxKeyWordsToProcess;
            isSuccess = int.TryParse(ConfigurationManager.AppSettings["maxKeyWordsToProcess"], 
                                                                    out maxKeyWordsToProcess);
            if (!isSuccess) maxKeyWordsToProcess = int.MaxValue;

            List<KeywordStatistic> statistics = KeywordAnalysisHelper.KeywordAnalysis(forums, 
                                                                                     maxKeyWordsToDisplay, 
                                                                                     maxKeyWordsToProcess, 
                                                                                     objcontext);

            return RedirectToAction("KeywordBasedStatistics", "Display");
        }

        /*Computes and lists the statistics of the appearance of keywords*/
        public ActionResult KeywordBasedStatistics()
        {
            List<KeywordStatistic> statistics = objcontext.KeyWordStatistics.ToList();
            statistics = statistics.GroupBy(s => s.KeyWord)
                                    .Select(res => new KeywordStatistic
                                    {
                                        KeyWord = res.First().KeyWord,
                                        KeywordOccurences = res.Sum(f => f.KeywordOccurences),
                                        NumberOfForums = res.Select(f => f.ForumName).Distinct().ToList().Count,
                                        NumberOfQuestions = res.Count()
                                    }).ToList();
            return View(statistics);
        }

        /*Displayes forum information in detail*/
        public ActionResult ReadmoreContent(int forumId)
        {
            var forum = objcontext.ForumDetails
                         .Include(x => x.Questions.Select(y => y.Replies))
                         .ToList().First(f => f.ForumDetailID == forumId);
            return View(forum);


        }

        /*Displayes keyword occurence information in detail*/
        public ActionResult KeywordDetails(string key)
        {
            List<KeywordStatistic> statistics = objcontext.KeyWordStatistics.ToList();
            statistics = statistics.Where(st => st.KeyWord == key).ToList()
                                    .GroupBy(s => s.ForumName)
                                    .Select(res => new KeywordStatistic
                                    {
                                        KeyWord = res.First().KeyWord,
                                        ForumName = res.First().ForumName,
                                        KeywordOccurences = res.Sum(f => f.KeywordOccurences),
                                        NumberOfQuestions = res.Count()
                                    }).ToList();
            return View(statistics);


        }


        private void CleanUpBackendData()
        {
            objcontext.Database.ExecuteSqlCommand("DELETE FROM KeywordStatistics");
        }
    }

}