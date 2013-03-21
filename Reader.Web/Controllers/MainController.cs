namespace Reader.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using AttributeRouting.Web.Mvc;
    using NBrowserID;
    using Reader.Services;
    using NReadability;
    using System.Linq;

    public class MainController : Controller
    {
        #region GET  /

        public ActionResult Index ()
        {
            if (User.Identity.IsAuthenticated)
            {   
                var feeds = Reader.Services.Feeds.FeedsFor(User)
                    .Select(p => p.articles)
                    .ToList()
                    .SelectMany(i => i)
                    .OrderByDescending(k => k.published);

                var transcoder = new NReadabilityWebTranscoder();
                var tr2 = new NReadabilityTranscoder(ReadingStyle.Newspaper, ReadingMargin.Medium, ReadingSize.Medium);
                bool outy = false;


                var model = feeds.Select(p => new Models.ArticleItem()
                {
                    Article = p,
                    Content = p.content
                });

                return View(model);
            }
            else
            {
                return View("Splash");
            }
        }

        #endregion

        #region POST /sign-in

        [POST("/sign-in")]
        public JsonResult SignIn(string assertion)
        {
            var authentication = new BrowserIDAuthentication();
            var verificationResult = authentication.Verify(assertion);
            if (verificationResult.IsVerified)
            {
                string email = verificationResult.Email.Trim().ToLower();
                if (!Accounts.Exists(email) && !Accounts.Create(email, 10.0M))
                {
                    return Json(false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    return Json(true);
                }
            }

            return Json(null);
        }

        #endregion

        #region GET  /sign-out

        [GET("/sign-out")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        #endregion
    }
}