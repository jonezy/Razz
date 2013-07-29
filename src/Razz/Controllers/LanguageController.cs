using System.Web.Mvc;

namespace Razz.Controllers {
  public class SetLanguageController : BaseController {
    public ActionResult Index(string lang) {
      string returnUrl = "/";
      if (Request.UrlReferrer != null) {
        returnUrl = Request.UrlReferrer.ToString();
      }

      GlobalBase.SetLanguage(lang);

      ViewBag.CheckDomain = false;

      return Redirect(returnUrl);
    }
  }
}
