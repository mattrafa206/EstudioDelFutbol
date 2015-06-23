//=============================================================================
// Sistema      : Clases de utilidades para paginas ASP.NET
// Archivo      : Utils.cs
// Autor        : E Beretta (e.beretta@assista.com)
// Actualizado  : 21/10/2009
// Compilador   : Microsoft Visual C#
//
// Funciones generales de uso común.
//
// Version  Fecha       Autor   Comentarios
// ============================================================================
// 1.0.0.0  22/10/2009  ENB     Creación de clase
//=============================================================================

using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

using System.Xml;
using System.Xml.Serialization;

namespace EstudioDelFutbol.CommonWeb
{
  /// <summary>
  /// Clase con funciones útiles.
  /// </summary>
  public static class Utils {

    /// <summary>
    /// Devuelve el Id de Usuario cargado en el ticket de autenticacion
    /// </summary>
    /// <param name="pPage">Page</param>
    /// <returns>IdUsuario</returns>
    public static Int32 GetUserIDFromTicket(Page pPage) {
      FormsIdentity ident = (FormsIdentity)pPage.User.Identity;
      FormsAuthenticationTicket ticket = ident.Ticket;
      String strObjUserInfo = ticket.UserData.Split('#')[1];

      StringReader stream = new StringReader(strObjUserInfo);
      XmlTextReader reader = new XmlTextReader(stream);
      XmlSerializer xs = new XmlSerializer(typeof(UserInfo));
      UserInfo userInfo = (UserInfo)xs.Deserialize(reader);

      return userInfo.IdClub;
    }

    /// <summary>
    /// Obtiene el objeto con informacion del usuario cargado en el ticket de autenticacion
    /// </summary>
    /// <param name="pPage">Page</param>
    /// <returns>UserInfo</returns>
    public static UserInfo GetUserInfoFromTicket(Page pPage) {
      FormsIdentity ident = (FormsIdentity)pPage.User.Identity;
      FormsAuthenticationTicket ticket = ident.Ticket;
      String strObjUserInfo = ticket.UserData.Split('#')[1];

      StringReader stream = new StringReader(strObjUserInfo);
      XmlTextReader reader = new XmlTextReader(stream);
      XmlSerializer xs = new XmlSerializer(typeof(UserInfo));
      UserInfo userInfo = (UserInfo)xs.Deserialize(reader);

      return userInfo;
    }

    public static UserInfo GetUserInfoFromTicket(System.Security.Principal.IIdentity Identity)
    {
        FormsIdentity ident = (FormsIdentity)Identity;
        FormsAuthenticationTicket ticket = ident.Ticket;
        String strObjUserInfo = ticket.UserData.Split('#')[1];

        StringReader stream = new StringReader(strObjUserInfo);
        XmlTextReader reader = new XmlTextReader(stream);
        XmlSerializer xs = new XmlSerializer(typeof(UserInfo));
        UserInfo userInfo = (UserInfo)xs.Deserialize(reader);

        return userInfo;
    }

  }

}
