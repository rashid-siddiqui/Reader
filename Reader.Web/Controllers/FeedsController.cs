namespace Reader.Web.Controllers
{
    using System.Web.Mvc;
    using Reader.Web.Models;
    using AttributeRouting.Web.Mvc;
    using Reader.Services;
    using System.Linq;

    public class FeedsController : Controller
    {
        [GET("/feeds")]
        public JsonResult Get()
        {
            var feeds = Feeds.FeedsFor(User)
                .Select(p => p.articles)
                .ToList()
                .SelectMany(i => i)
                .OrderByDescending(k => k.published);
            return Json(feeds, JsonRequestBehavior.AllowGet);
        }

        [POST("/feeds")]
        public JsonResult Add(AddFeedModel model)
        {
            if (ModelState.IsValid)
            {
                var feed = Feeds.GetOrCreate(model.FeedUrl);
                if (feed != null)
                {
                    return Json(Accounts.AddFeedTo(User, feed.id));
                }
            }

            return Json(false);
        }

    }
}