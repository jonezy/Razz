# Razz

Some helpful stuff to make rapidly prototyping asp.net mvc projects easy peasy.

# How to use it.


## Global.asax.cs

Change the class definition to look like this

    public class MvcApplication : Razz.GlobalBase
    
## Controllers

Razz includes a simple controller that will handle requests that come from a RazzRoute.  To enable this controller
add this route to your global.asax.cs

      routes.MapRoute(
          "RazzRoute",
          "{lang}/{*pathInfo}",
          new { controller = "Page", action = "Index", lang = "en" },
          new string[] { "Razz.Controllers" }
      );
      
Now a url like this

    /about/team/
    
Will map to 

    Views
      About
        Team.ascx

