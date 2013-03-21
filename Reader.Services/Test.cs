using System;
using QDFeedParser;

namespace Reader.Services
{
    class Test
    {
        void x()
        {
            var uri = new Uri("http://www.kungfugrippe.com/rss", UriKind.Absolute);
            var ff = new HttpFeedFactory();
            if (ff.PingFeed(uri))
            {
                var feed = ff.CreateFeed(uri);
                
            }
        }
    }
}
