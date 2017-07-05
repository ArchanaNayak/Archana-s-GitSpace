using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UOA.ForumData.Core
{
    
    public class ForumKeyWord
    {
        public ForumKeyWord()
        {}
       
        public int ForumKeywordID { get; set; }
        [NotMapped]
        public string KeyWord { get; set; }
        
    }
}
