using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    public class CrawledPage
    {
        
       
        public CrawledPage() { }

        
        private int _size;
        private string _text;
        private string _url;


        /* Public Properties */
        public int CrawledPageID { get; set; }
        public int Size
        {
            get { return _size; }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _size = value.Length;
            }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }                     
    }
}