using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Razz {
  public class GlobalBase : System.Web.HttpApplication {

    public static string Lang {
      get {
        try {
          if (HttpContext.Current.Session["Culture"].ToString().ToUpper() == "FR-CA")
            return "fr";

          return HttpContext.Current.Session["Culture"].ToString();
        } catch { return "en"; }
      }
      set {
        if (value == "fr")
          value = "fr-ca";
        try {
          HttpContext.Current.Session["Culture"] = new CultureInfo(value);
        } catch { }
      }
    }

    public static string SwitchUrl {get;set;}

    public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
      filters.Add(new HandleErrorAttribute());
      filters.Add(new CompressFilter());
    }

    protected virtual void Application_Start() {
      RegisterGlobalFilters(GlobalFilters.Filters);
    }

    protected void Application_AcquireRequestState(object sender, EventArgs e) {
      if (HttpContext.Current.Session != null) {
        if (!HttpContext.Current.Request.Url.AbsoluteUri.Contains(".css")
          || !HttpContext.Current.Request.Url.AbsoluteUri.Contains(".js")
          || !HttpContext.Current.Request.Url.AbsoluteUri.Contains(".gif")
          || !HttpContext.Current.Request.Url.AbsoluteUri.Contains(".jpg")
          || !HttpContext.Current.Request.Url.AbsoluteUri.Contains(".png")) {
         
          CheckDomain();

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

    private void CheckDomain() {
      // for now, we'll use string arrays to store our valid domains.
      string[] enUrls = ConfigurationManager.AppSettings["enUrls"].Split(',');
      string[] frUrls = ConfigurationManager.AppSettings["frUrls"].Split(',');
      bool enFound = false, frFound = false;

      string requestUrl = HttpContext.Current.Request.Url.Host;
      // removes www. if it's present.
      if (requestUrl.StartsWith("www.")) requestUrl = requestUrl.Remove(0, 4);

      foreach (var item in enUrls) {
        if (item.ToUpper() == requestUrl.ToUpper()) {
          enFound = true;
          break;
        }
      }

      foreach (var item in frUrls) {
        if (item.ToUpper() == requestUrl.ToUpper()) {
          frFound = true;
          break;
        }
      }

      if (enFound) {
        SetLanguage("en");
        SwitchUrl = string.Format("http://{0}", frUrls[frUrls.Length - 1]);
        return;
      }

      if (frFound) {
        SetLanguage("fr");
        SwitchUrl = string.Format("http://{0}", enUrls[enUrls.Length - 1]);
        return;
      }

      // if we've gotten this far, we can't match the requested url, so we'll just carry on as english
      SetLanguage("en");
    }

    public static void SetLanguage(string lang) {
      if (string.IsNullOrEmpty(lang)) lang = "en";
      if (lang.Equals("fr")) lang = "fr-ca";
      if (!lang.Equals(GlobalBase.Lang)) GlobalBase.Lang = lang;
    }
  }
}