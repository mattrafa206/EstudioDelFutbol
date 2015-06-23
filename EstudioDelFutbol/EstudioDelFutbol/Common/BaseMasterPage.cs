using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EstudioDelFutbol.Logic;
using EstudioDelFutbol.Common;
using EstudioDelFutbol.Logger;

namespace EstudioDelFutbol.CommonWeb
{
    public class BaseMasterPage : System.Web.UI.MasterPage
    {
       private BizServer _bizServer = null;

       protected BizServer bizServer
        {
            get { return _bizServer; }
        }

        protected Log logger
        {
            get { return _bizServer.Log; }
        }

        public BaseMasterPage()
        {
            _bizServer = (BizServer)HttpContext.Current.Application["BIZSERVER"];
        }

    }
}