using System;
using System.Web;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.Security;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;
using EstudioDelFutbol.Logic;
using EstudioDelFutbol.Common;
using System.Web.Configuration;

namespace EstudioDelFutbol.CommonWeb
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    public class Security
    {

        /// <summary>
        /// Validate WebClientApp User
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <param name="companyID"></param>
        /// <param name="userInfo"></param>
        /// <param name="bizServer"></param>
        /// <returns></returns>
        private static String ValidateUser(String userName, String password, ref List<string> lstRoles, ref UserInfo userInfo, BizServer bizServer)
        {

            DataTable users;
            using (User user = new User(bizServer))
            {
                object result = new DataTable();
                user.Login(userName, password, ref result);
                users = (DataTable)result;
            }

            if (users.Rows.Count > 0)
            {
                //Get ClientApp UserInfo           
                userInfo.IdClub = Convert.ToInt32(users.Rows[0]["ClubID"]);
                userInfo.Nombre = users.Rows[0]["ClubName"].ToString();
                userInfo.Email = users.Rows[0]["Mail"].ToString();
                userInfo.InternalPath = WebConfigurationManager.AppSettings["InternalMainPath"] + userName + "_" + userInfo.IdClub.ToString() + "\\";
                userInfo.ExternalPath = WebConfigurationManager.AppSettings["ExternalMainPath"] + userName + "_" + userInfo.IdClub.ToString() + "/";
                userInfo.Picture = userInfo.ExternalPath + userName + ".png";
                string NextMatchPath = Path.Combine(userInfo.InternalPath, "Rival");
                userInfo.NextMatch = Path.GetFileNameWithoutExtension(new FileInfo(Directory.GetFiles(NextMatchPath)[0]).Name);

                HttpContext.Current.Session[Consts.USER_INFO] = userInfo;

                lstRoles.Add("AppClient");

                return string.Empty; //Usuario Logueado.
            }
            else
            {
                return "Usuario y/o Contraseña incorrectos";
            }

        }

        /// <summary>
        /// Asigna roles a un usuario
        /// </summary>
        /// <param name="strUsername">Usuario</param>
        /// <param name="lstRoles">Lista de Roles</param>
        /// <returns>string de roles</returns>
        private static String AssignRoles(String strUsername, List<string> lstRoles)
        {
            //Devuelve la lista de roles a los que pertenece el usuario separada por |
            string strRoles = String.Empty;
            foreach (string str in lstRoles)
            {
                strRoles += str.Trim() + "|";
            }
            int largo = strRoles.Length;
            strRoles = strRoles.Substring(0, largo - 1);
            return strRoles;
        }

        /// <summary>
        /// Autentica un Usuario
        /// </summary>
        /// <param name="strUsername">Usuario</param>
        /// <param name="strPassword">Contraseña</param>
        public static String Authenticate(String userName, String passsword, BizServer bizServer)
        {
            List<string> lstRoles = new List<string>();
            UserInfo userInfo = new UserInfo();
            String LoginSuccess = ValidateUser(userName, passsword, ref lstRoles, ref userInfo, bizServer);
            if (LoginSuccess == String.Empty)
            {
                FormsAuthentication.Initialize();
                String strRole = AssignRoles(userName, lstRoles);

                StringWriter writer = new StringWriter();
                XmlSerializer xsl = new XmlSerializer(userInfo.GetType());
                xsl.Serialize(writer, userInfo);

                strRole += "#" + writer.ToString();

                //AddMinutes determina cuanto tiempo el usuario estará logueado despues de dejar el sitio si no se deslogueo.
                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(60), false, strRole, FormsAuthentication.FormsCookiePath);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

                return LoginSuccess;
            }
            else return LoginSuccess;
        }

    }

}
