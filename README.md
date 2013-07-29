# Razz

Some helpful stuff to make rapidly prototyping asp.net mvc projects easy peasy.  Worry about building your app 
and not writing pointless controller code.

Razz can also *act* as a static site, since the controller's responsibility is to locate the view and only locate 
the view it's almost like a static site.


# How to use it.

## Global.asax.cs

Change the class definition to look like this

    public class MvcApplication : Razz.GlobalBase
    
## Controllers

There are 2 ways to get Razz.Controllers working on your site.  Both are super simple and easy to setup.

### Use the included PageController

Razz includes a simple controller that will handle requests that come from a RazzRoute.  To enable this controller
add this route to your global.asax.cs

      routes.MapRoute(
          "RazzRoute",
          "{*pathInfo}",
          new { controller = "Page", action = "Index"},
          new string[] { "Razz.Controllers" }
      );

### Inherit from BaseController
    
    public HomeController : Razz.Controllers.BaseController {
        public ActionResult Index() {
            return View();
        }
    }
    
In both of the above examples the below will be true:

Now a url like this

    /about/team/
    
Will map to 

    Views
      About
        Team.ascx

# Languages

Razz also supports setting the sites language based on the domain that is used to load the site.  

Say for instance you had a site that you wanted to support english and french on (these are currently the only 
supported languages in Razz).

    Web.config
    <appSettings>
        <add key="enUrls" value="www.example.com" />
        <add key="frUrls" value="www.examplé.com" />
    </appSettings>
    
Now the site will set the Session["Culture"] properly depending on what domain you use to access the site.
