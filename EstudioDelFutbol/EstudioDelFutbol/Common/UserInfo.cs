using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;


namespace EstudioDelFutbol.CommonWeb
{
    /// <summary>
    /// Summary description for UserInfo
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        #region Variables privadas

        Int32 _idClub;
        string _nombre;
        string _email;
        string _picture;
        string _internalPath;
        string _externalPath;
        string _nextMatch;

        #endregion Variables privadas

        #region Propiedades


        public Int32 IdClub
        {
            get { return _idClub; }
            set { _idClub = value; }
        }



        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public string InternalPath
        {
            get { return _internalPath; }
            set { _internalPath = value; }
        }

        public string ExternalPath
        {
            get { return _externalPath; }
            set { _externalPath = value; }
        }

        public string NextMatch
        {
            get { return _nextMatch; }
            set { _nextMatch = value; }
        }



        #endregion Propiedades

        /// <summary>
        /// Constructor
        /// </summary>
        public UserInfo()
        {
        }
    }

}
