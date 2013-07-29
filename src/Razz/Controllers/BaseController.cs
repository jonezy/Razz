using System.Web.Mvc;

namespace Razz.Controllers {
  public class BaseController : Controller {

    // this takes the incoming request and picks apart the *pathInfo to figure out what
    // controller/action (ViewFolder/ViewFile) combo should be loaded.
    // In fact that is a better way of thinking of it, anything after the domain
    // maps to a view folder / view file
    // 
    // 
    // /                  => views/home/index.cshtml
    // /about/            => views/about/index.cshtml
    // /products/oranges  => views/products/oranges.cshtml
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
