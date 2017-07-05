using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    public class ForumDetail
    {
        
        
        private string _name;
        private string _url;


        private List<Question> _questions = new List<Question>();

        /* Public properties */
        public int ForumDetailID { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public List<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }
    }
}