namespace Reader.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using AttributeRouting.Web.Mvc;
    using NBrowserID;
    using Reader.Services;

    public class MainController : Controller
    {
        #region GET  /

        public ActionResult Index ()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
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