using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace Razz {
  public class GlobalBase : System.Web.HttpApplication {
    protected void Application_AcquireRequestState(object sender, EventArgs e) {
      if (HttpContext.Current.Session != null) {
        CultureInfo ci = (CultureInfo)this.Session["Culture"];
        //Checking first if there is no value in session and set default language this can happen for first user's request
        if (ci == null) {
          string langName = "en";

          if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0) {
            langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
          }

          ci = new CultureInfo(langName);

          this.Session["Culture"] = ci;
        }

        Thread.CurrentThread.CurrentUICulture = ci;
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
      }
    }
  }
}
