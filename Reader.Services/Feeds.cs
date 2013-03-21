namespace Reader.Services
{
    using System;
    using System.Linq;
    using QDFeedParser;
    using Reader.Data;
    using System.Security.Principal;

    public static class Feeds
    {
        private static EntityRepository<FeedEntity> Entities
        {
            get
            {
                return new EntityRepository<FeedEntity>();
            }
        }

        public static bool ExistsByUri(Uri feed_uri)
        {
            return Feeds.Entities.Any(
                item => item.uri == feed_uri
            );
        }

        public static FeedEntity GetOrCreate(Uri feed_uri)
        {
            var feed = Feeds.Entities.SingleOrDefault(f => f.uri == feed_uri);
            if (feed != null) {
                return feed;
            }

            IFeedFactory factory = new HttpFeedFactory();
            if (!factory.PingFeed(feed_uri)) {
                return null;
            }

            var new_feed = factory.CreateFeed(feed_uri);
            feed = new FeedEntity()
            {
                link = new Uri(new_feed.Link),
                title = new_feed.Title,
                uri = feed_uri,
                articles = new_feed.Items.Select(item => new Article() {
                    article_id = item.Id,
                    author = item.Author,
                    categories = item.Categories.ToList(),
                    content = item.Content,
                    link = new Uri(item.Link),
                    published = item.DatePublished,
                    title = item.Title
                }).ToList()
            };

            if (Feeds.Entities.Insert(feed))
            {
                return Feeds.Entities.SingleOrDefault(f => f.uri == feed_uri);
            }
            else
            {
                return null;
            }
        }

        public static IQueryable<FeedEntity> FeedsFor(IPrincipal User)
        {
            var account = Accounts.Get(User);
            var feed_ids = account.feeds.Select(id => id.Key);
            return Feeds.Entities.Where(
                f => feed_ids.Contains(f.id)
            );
        }
    }
}
