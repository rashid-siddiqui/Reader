namespace Reader.Data
{
    using System;
    using System.Collections.Generic;

    public sealed class FeedEntity : EntityBase
    {
        public Uri uri { get; set; }

        public Uri link { get; set; }

        public DateTime updated { get; set; }

        public string title { get; set; }

        public List<Article> articles { get; set; }

        public override string[] index_keys
        {
            get {
                return new[] {
                    "uri"
                };
            }
        }
    }

    public sealed class Article
    {
        public string author { get; set; }

        public List<string> categories { get; set; }

        public string content { get; set; }

        public DateTime published { get; set; }

        public string article_id { get; set; }

        public Uri link { get; set; }

        public string title { get; set; }
    }
}