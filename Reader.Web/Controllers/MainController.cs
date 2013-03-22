namespace Reader.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using AttributeRouting.Web.Mvc;
    using Reader.Web.Models;
    using Reader.Services;

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
        public JsonResult SignIn(SignInViewModel viewmodel)
        {
            //
            if (!ModelState.IsValid) return Json(false);

            //
            if (!Accounts.Exists(viewmodel.Email)) {
                if (!Accounts.Register(viewmodel.Email, viewmodel.Password, 10.0M)) return Json(false);
            }

            //
            if (!Accounts.CanSignIn(viewmodel.Email, viewmodel.Password)) return Json(false);

            FormsAuthentication.SetAuthCookie(viewmodel.Email.ToLower(), false);
            return Json(true);
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