using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UOA.ForumData.Analysis.DAL;
using UOA.ForumData.Core;
using UOA.ForumData.TopicAnalyser;

namespace UOA.ForumData.Analysis.Helpers
{
    public static class KeywordAnalysisHelper
    {
        /* Craetes keyword dictionary, populates keywords into dictitionary and sorts the keywords based on highest number of occurences of keywords */
        public static List<KeywordStatistic> KeywordAnalysis(List<ForumDetail> forums, int maxKeyWordsToDisplay, int maxKeyWordToProcess, OnlineSupportForumDatabaseContext objcontext)
        {
            KeywordAnalyzer ka = new KeywordAnalyzer();
            Dictionary<string, List<ForumDataAnalysis>> keywordsDictionary = new Dictionary<string, List<ForumDataAnalysis>>();

            foreach (var forum in forums)
            {
                foreach (var question in forum.Questions)
                {
                    KeywordAnalysis q1 = ka.Analyze(question.Text);
                    keywordsDictionary = PopulateKeyWords(keywordsDictionary, q1, forum, question, maxKeyWordToProcess);
                    KeywordAnalysis q2 = ka.Analyze(question.Content);
                    keywordsDictionary = PopulateKeyWords(keywordsDictionary, q2, forum, question, maxKeyWordToProcess);
                    foreach (var reply in question.Replies)
                    {
                        KeywordAnalysis r1 = ka.Analyze(reply.Text);
                        keywordsDictionary = PopulateKeyWords(keywordsDictionary, r1, forum, question, maxKeyWordToProcess);
                    }
                }
            }

            var sortedKeywordsDitctionary = SortDictinaryByMaximumMatch(keywordsDictionary, maxKeyWordsToDisplay);
            return InsertKeywordStatistics(sortedKeywordsDitctionary, objcontext);

        }

        /* Organises the list of keywords to be displayed */
        private static List<KeywordStatistic> InsertKeywordStatistics(Dictionary<string, List<ForumDataAnalysis>> sortedKeywordsDitctionary, OnlineSupportForumDatabaseContext objcontext)
        {
            List<KeywordStatistic> stats = new List<KeywordStatistic>();
            foreach (var key in sortedKeywordsDitctionary.Keys)
            {
                var forumKeyWord = new ForumKeyWord();
                forumKeyWord.KeyWord = key;
                foreach (var fa in sortedKeywordsDitctionary[key])
                {
                    foreach (var forum in fa.forums)
                    {
                        foreach (var question in forum.Questions)
                        {
                            KeywordStatistic keyStat = new KeywordStatistic()
                            {
                                KeyWord = key,
                                ForumName = forum.Name,
                                ForumUrl = forum.Url,
                                NumberOfQuestions = forum.Questions.Count

                            };

                            keyStat.QuestionName = question.Text;
                            keyStat.QuestionUrl = question.Url;
                            keyStat.KeywordOccurences = ExtractKeyWordOccurences(key, question);
                            stats.Add(keyStat);
                            objcontext.KeyWordStatistics.Add(keyStat);
                        }
                        if (forum.Questions.Count <= 0)
                        {
                            KeywordStatistic keyStat = new KeywordStatistic()
                            {
                                KeyWord = key,
                                ForumName = forum.Name,
                                ForumUrl = forum.Url,
                                NumberOfQuestions = forum.Questions.Count

                            };
                            stats.Add(keyStat);
                            objcontext.KeyWordStatistics.Add(keyStat);
                        }


                    }

                }
                objcontext.SaveChanges();


            }
            return stats;
        }

        /* Extracts the keyword occurences*/
        private static int ExtractKeyWordOccurences(string key, Question question)
        {
            int number_of_occurences = 0;

            number_of_occurences += Regex.Matches(Regex.Escape(question.Text.ToLower()), key.ToLower()).Count;
            number_of_occurences += Regex.Matches(Regex.Escape(question.Content.ToLower()), key.ToLower()).Count;
            foreach (var reply in question.Replies)
            {
                number_of_occurences += Regex.Matches(Regex.Escape(reply.Text.ToLower()), key.ToLower()).Count;
            }

            return number_of_occurences;
        }


        private static Dictionary<string, List<ForumDataAnalysis>> PopulateKeyWords(Dictionary<string, List<ForumDataAnalysis>> keywordsDitctionary, KeywordAnalysis kw, ForumDetail forum, Question question, int maxKeyWordToProcess)
        {
            int counter = 0;
            foreach (var keyword in kw.Keywords)
            {
                counter++;
                if (counter >= maxKeyWordToProcess) return keywordsDitctionary;
                if (keywordsDitctionary.ContainsKey(keyword.Word))
                {
                                     
                    var keyItem = keywordsDitctionary[keyword.Word];
                    var fda = new ForumDataAnalysis();
                    fda.Rank = keyword.Rank;
                    if (!CheckIfForumAlreadyExists(keyItem, forum))
                    {
                        ForumDetail forumToAdd = new ForumDetail();
                        forumToAdd.ForumDetailID = forum.ForumDetailID;
                        forumToAdd.Name = forum.Name;
                        forumToAdd.Url = forum.Url;
                        forumToAdd.Questions = new List<Question>();
                        forumToAdd.Questions.Add(question);
                        fda.forums.Add(forumToAdd);

                    }
                    if (!CheckIfQuestionAlreadyExists(keyItem, question, forum))
                    {
                        var forumtoAdd = ExtractForumFromDictionary(keyItem, forum.ForumDetailID);
                        if (forumtoAdd != null) forumtoAdd.Questions.Add(question);
                    }
                    keyItem.Add(fda);

                }
                else
                {
                    var fdaList = new List<ForumDataAnalysis>();
                    var fda = new ForumDataAnalysis();
                    fda.Rank = keyword.Rank;
                    ForumDetail forumToAdd = new ForumDetail();
                    forumToAdd.ForumDetailID = forum.ForumDetailID;
                    forumToAdd.Name = forum.Name;
                    forumToAdd.Url = forum.Url;
                    forumToAdd.Questions = new List<Question>();
                    forumToAdd.Questions.Add(question);
                    fda.forums.Add(forumToAdd);
                    fdaList.Add(fda);
                    keywordsDitctionary.Add(keyword.Word, fdaList);
                }
            }
            return keywordsDitctionary;
        }
        private static ForumDetail ExtractForumFromDictionary(List<ForumDataAnalysis> keyItem, int ForumId)
        {
            ForumDetail forumToAdd = null;
            foreach (var item in keyItem)
            {
                try
                {
                    forumToAdd = item.forums.Where(f => f.ForumDetailID == ForumId).First();
                    return forumToAdd;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                catch (ArgumentNullException)
                {
                    return null;
                }

            }

            return forumToAdd;
        }

        /* Checks for duplicate question entries */
        private static bool CheckIfQuestionAlreadyExists(List<ForumDataAnalysis> keyItem, Question question, ForumDetail forum)
        {
            bool isMathFound = false;
            foreach (var item in keyItem)
            {
                try
                {
                    var forumMatched = item.forums.Where(f => f.ForumDetailID == forum.ForumDetailID).First();
                    return forumMatched.Questions.Exists(q => q.QuestionID == question.QuestionID);
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
                catch (ArgumentNullException)
                {
                    return false;
                }
            }
            return isMathFound;
        }

        /* Checks for duplicate forum entries */
        private static bool CheckIfForumAlreadyExists(List<ForumDataAnalysis> keyItem, ForumDetail forum)
        {
            bool isMathFound = false;
            foreach (var item in keyItem)
            {
                if (item.forums.Exists(f => f.ForumDetailID == forum.ForumDetailID)) return true;
            }
            return isMathFound;
        }

        /* Performs sorting of the keywords in descending order */
        private static Dictionary<string, List<ForumDataAnalysis>> SortDictinaryByMaximumMatch(Dictionary<string, List<ForumDataAnalysis>> keywordsDitctionary, int maxKeyWordsToDisplay)
        {
            Dictionary<string, List<ForumDataAnalysis>> sortedKeywordsDitctionary = new Dictionary<string, List<ForumDataAnalysis>>();
            var sortResult = keywordsDitctionary.OrderByDescending(x => x.Value.Count);
            int counter = 0;

            foreach (var k in sortResult)
            {
                counter++;
                if (counter <= maxKeyWordsToDisplay)
                {
                    if (IsSimilarKeyWord(k.Key, sortedKeywordsDitctionary)) { counter--; continue; };
                    if (IsCommonword(k.Key)) { counter--; continue; };
                    sortedKeywordsDitctionary.Add(k.Key, keywordsDitctionary[k.Key]);
                }
            }

            return sortedKeywordsDitctionary;
        }

        private static bool IsSimilarKeyWord(string keyword, Dictionary<string, List<ForumDataAnalysis>> keywordsDitctionary)
        {
            bool isMatchFound = keywordsDitctionary.Keys.Any(currentKey => currentKey.ToLower().Contains(keyword.ToLower()));
            if (isMatchFound) return true;

            foreach (string key in keywordsDitctionary.Keys)
            {
                isMatchFound = keyword.ToLower().Contains(key.ToLower());
                if (isMatchFound) return true;
            }
            return false;

        }
        private static bool IsCommonword(string keyword)
        {
            bool isMatchFound = new StemWord().commonwords.Exists(k => k.ToLower() == keyword.ToLower());
            return isMatchFound;

        }


    }
}