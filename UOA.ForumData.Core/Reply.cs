using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    public class Reply
    {
        /* Private Instance Fields */
        private string _text;
        private string _date;
        private string _relpiedBy;
        private bool _isBestReply;


       
        public int ReplyID { get; set; }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string RepliedBy
        {
            get { return _relpiedBy; }
            set { _relpiedBy = value; }
        }
        public bool IsaBestReply
        {
            get { return _isBestReply; }
            set { _isBestReply = value; }
        }
    }
}