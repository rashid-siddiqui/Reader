namespace Reader.Web.Controllers
{
    using System.Web.Mvc;
    using AttributeRouting.Web.Mvc;
    using System;

    [Authorize]
    public class PagesController : Controller
    {
        #region GET  /pages

        [GET("/pages")]
        public PartialViewResult Index()
        {
            return PartialView();
        }

        #endregion

        #region GET  /pages/bookmarklet

        public PartialViewResult BookmarkletEditor()
        {
            return PartialView();
        }

        #endregion

        #region GET  /pages/save

        public ActionResult Save(string bmkey, Uri url)
        {
            return Json(false);
        }

        #endregion
    }
}