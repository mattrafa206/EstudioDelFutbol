using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using EstudioDelFutbol.Common;
using EstudioDelFutbol.Logger;

namespace EstudioDelFutbol
{
    public class BaseController : Controller
    {
        private BizServer _bizServer = null;

        public BaseController()
        {
            _bizServer = GetBizServer((BizServer)System.Web.HttpContext.Current.Application["BIZSERVER"], System.Web.HttpContext.Current);
        }

        protected BizServer bizServer 
        {
            get { return _bizServer; }
        }

        protected Log logger
        {
            get { return _bizServer.Log; }
        }

        protected bool IsAuthenticated
        {
            get { return System.Web.HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        protected void AddModelStateValue(string key, object value)
        {

            var modelState = new ModelState();
            modelState.Value = new ValueProviderResult(value, String.Empty, System.Globalization.CultureInfo.InvariantCulture);
            ModelState.Add(key, modelState);

        }

        protected void CustomException(Exception ex, string message)
        {
            logger.TraceError(message + "(" + ex.Message + ": " + ex.StackTrace + ")", bizServer.Usuario.RemoteEndpoint);
        }

        private BizServer GetBizServer(BizServer genericBizServer, System.Web.HttpContext httpContext)
        {
            return new BizServer() { Log = genericBizServer.Log, DataBase = genericBizServer.DataBase, Usuario = new Usuario() { RemoteEndpoint = httpContext.Request.UserHostAddress } };
        }

        protected List<T> GetCustomObjectList<T>(string json)
        {
            return (List<T>)new JavaScriptSerializer().Deserialize(json, typeof(List<T>));
        }

        
    }
}