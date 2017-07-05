using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    public class Question
    {
        /* Private Instance Fields */
        private string _text;
        private string _content;
        private string _date;
        private string _author;
        private string _url;

        private List<Reply> _replies = new List<Reply>();


        /* Public properties */
        public int QuestionID { get; set; }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public List<Reply> Replies
        {
            get { return _replies; }
            set { _replies = value; }
        }
        
    }
}