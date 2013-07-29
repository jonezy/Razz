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

      if (language != null && !language.Equals(GlobalBase.Lang)) {
        GlobalBase.SetLanguage(language.ToString());
        Response.Redirect(this.Request.Url.ToString());
      }

      this.ControllerContext.RouteData.Values["controller"] = "home";
      this.ControllerContext.RouteData.Values["action"] = "index";

      if (this.RouteData.Values["pathInfo"] != null) {
        string[] pathParts = this.RouteData.Values["pathInfo"].ToString().Split('/');
        this.ControllerContext.RouteData.Values["controller"] = pathParts[0];
        this.ControllerContext.RouteData.Values["action"] = pathParts.Length > 1 && !string.IsNullOrEmpty(pathParts[1]) ? pathParts[1] : "index";
      }

      base.OnActionExecuting(filterContext);
    }
  }
}
