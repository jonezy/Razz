using System.Web.Mvc;

namespace Razz.Controllers {
  public class PageController : BaseController {

    // controller and action are set in BaseController.  This is a simple pass through
    // so that mvc.net will render the correct view.
    public ActionResult Index() {
      return View();
    }

  }
}
