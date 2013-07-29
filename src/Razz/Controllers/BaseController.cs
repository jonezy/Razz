using System.Globalization;
using System.Web.Mvc;

namespace Razz.Controllers {
  public class BaseController : Controller {
    protected override void OnActionExecuting(ActionExecutingContext filterContext) {
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
