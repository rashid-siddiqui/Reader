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
        public ActionResult SignIn(SignInViewModel viewmodel)
        {
            // Ensure model is valid
            if (!ModelState.IsValid)
            {
                return View("Splash");
            }

            // Create an account if not exists
            if (!Accounts.Exists(viewmodel.Email)) 
            {
                if (!Accounts.Register(viewmodel.Email, viewmodel.Password, 10.0M))
                {
                    ModelState.AddModelError(string.Empty, "There was an error registering accounts.");
                    return View("Splash");
                }
            }

            // Sign the account in
            if (!Accounts.CanSignIn(viewmodel.Email, viewmodel.Password))
            {
                ModelState.AddModelError(string.Empty, "The username or password provided is incorrect.");
                return View("Splash");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(viewmodel.Email.ToLower(), false);
                return RedirectToAction("Index");
            }
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