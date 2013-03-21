namespace Reader.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using NBrowserID;
    using System.Web.Security;
    using AttributeRouting.Web.Mvc;

    public class MainController : Controller
    {
        
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

        
        [POST("/sign-in")]
        public ActionResult SignIn(string assertion)
        {
            var authentication = new BrowserIDAuthentication();
            var verificationResult = authentication.Verify(assertion);
            if (verificationResult.IsVerified)
            {
                string email = verificationResult.Email;
                FormsAuthentication.SetAuthCookie(email, false);
                return Json(new { email });
            }

            return Json(null);
        }

        [GET("/sign-out")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}