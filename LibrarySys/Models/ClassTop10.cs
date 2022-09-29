using LibrarySys.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySys.Models
{
    public class ClassTop10
    {
        public IEnumerable<Top10Author_Result> topauthor { get; set; }
        public IEnumerable<Top10Reader_Result> topreader { get; set; }
        public IEnumerable<Top10ReadedBook_Result> topbook { get; set; }
       
    }
}