using System.Globalization;
using System.Web.Mvc;

namespace Razz.Controllers {
  public class BaseController : Controller {

    protected string Language() {
      try {
        if (Session["Culture"].ToString().ToUpper() == "FR-CA") return "fr";
        return Session["Culture"].ToString();
      } catch { return "en"; }
    }

    protected void SetLanguage(string lang) {
      if (string.IsNullOrEmpty(lang)) lang = "en";
      if (lang.Equals("fr")) lang = "fr-ca";
      if (!lang.Equals(this.Language())) Session["Culture"] = new CultureInfo(lang);
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext) {
      string language = this.RouteData.Values["lang"].ToString();

      if (language != null && !language.Equals(this.Language())) {
        SetLanguage(language.ToString());
        Response.Redirect(this.Request.Url.ToString());
      }

      base.OnActionExecuting(filterContext);
    }
  }
}
