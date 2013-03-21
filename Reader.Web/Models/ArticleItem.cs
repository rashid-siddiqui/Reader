using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reader.Web.Models
{
    public class ArticleItem
    {
        public string Content { get; set; }

        public Reader.Data.Article Article { get; set; }
    }
}