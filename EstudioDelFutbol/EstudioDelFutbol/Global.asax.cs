using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EstudioDelFutbol
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Pages", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            #region Initialize

            EstudioDelFutbol.Logger.Log logger = null;

            try
            {
                string activity = Server.MapPath(@"~\App_Data") + "\\ACTIVITY.LOG";
                string errors = Server.MapPath(@"~\App_Data") + "\\ERRORS.LOG";

                //Build Logger
                logger = new EstudioDelFutbol.Logger.Log(activity, errors, 4096, 32, false);

                //Build BizServer
                var bizServer = new EstudioDelFutbol.Common.BizServer();
                bizServer.Log = logger;

                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(@"~\");
                System.Configuration.ConnectionStringSettings connString = null;
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                    connString = rootWebConfig.ConnectionStrings.ConnectionStrings["EstudioDelFutbolConnectionString"];
                else
                    throw new Exception("Invalid ConnectionString.");

                if (connString != null)
                {
                    var oDB = new EstudioDelFutbol.Common.DataBase();
                    oDB.ConnectionString = connString.ConnectionString;

                    //Set database information
                    bizServer.DataBase = oDB;
                }

                Application.Add("BIZSERVER", bizServer);
            }
            catch (Exception ex)
            {
                if (logger != null)
                    logger.TraceError(ex.Message + " " + ex.StackTrace);
            }

            #endregion
        }

        protected void Application_End()
        {
            try
            {
                if (Application["BIZSERVER"] != null)
                {
                    var bizServer = (EstudioDelFutbol.Common.BizServer)Application["BIZSERVER"];
                    bizServer.Log.CloseLog();
                    bizServer.Log = null;
                    bizServer = null;
                }
            }
            catch { }
        }
    }
}