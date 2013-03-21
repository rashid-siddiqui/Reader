using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reader.Web.Models
{
    public class AddFeedModel
    {
        [Required]
        public Uri FeedUrl { get; set; }
    }
}