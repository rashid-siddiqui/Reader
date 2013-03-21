namespace Reader.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddFeedModel
    {
        [Required]
        [DataType(DataType.Url)]
        public Uri FeedUrl { get; set; }
    }
}