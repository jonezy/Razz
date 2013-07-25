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

# Languages

Razz also comes with language support built in! It's english by default but if you include in the url like ~/fr/about/team~
The CultureInfo will be changed to fr-CA (i guess you could include any valid culture code and it would work). This means
you can take advantage of asp.net Resources to store language specific content.
