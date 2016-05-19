using ProjektHermods.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using System.Linq;

namespace ProjektHermods
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Session_Start(object sender, EventArgs e) //Startup
        {
            //Variabel för info om man är inloggad
            Session["IsLoggedIn"] = false;


        }

    }
}