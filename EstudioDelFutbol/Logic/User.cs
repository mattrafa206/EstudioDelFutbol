using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;
using System.Data.SqlTypes;
using EstudioDelFutbol.Data.ADONETDataAccess;
using EstudioDelFutbol.Logic;
using EstudioDelFutbol.Logic.BaseClass;
using EstudioDelFutbol.Logic.Utils;
using EstudioDelFutbol.Common;
using EstudioDelFutbol.Common.Security;
using EstudioDelFutbol.Logic.Resources;

namespace EstudioDelFutbol.Logic
{
	public class User : BaseClass.BaseClass
	{
		public User(BizServer bizSvr)
			: base(bizSvr, Assembly.GetCallingAssembly().GetName().Name, true)
		{
		}

		public User(BizServer bizSvr, bool createDBConnection)
			: base(bizSvr, Assembly.GetCallingAssembly().GetName().Name, createDBConnection)
		{
		}

		public User(BizServer bizSvr, DataAccess datAcc)
			: base(bizSvr, datAcc)
		{
		}


		/// <summary>
		/// Login
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="mobile"></param>
		/// <param name="CityID"></param>
		/// <param name="userPassword"></param>
		/// <param name="oResult"></param>
		public void Login(string userName, string userPassword, ref object oResult)
		{
			string strSQL = "";

			try
			{
				oDataAccess.ClearParameters();
                oDataAccess.AddParameter("UserName", userName);

				strSQL = "SELECT * FROM [Club] WITH(NOLOCK) WHERE UserName = ? AND Club.IsActive = 1";

				var dt = oDataAccess.GetDataTable(strSQL);

				if (dt.Rows.Count > 0)
				{
					if (!Convert.ToBoolean(dt.Rows[0]["IsActive"]))
					{
						throw new ValidationException(Messages.UserUnEnable);
					}
					if (!PasswordHash.ValidatePassword(userPassword, dt.Rows[0]["Password"].ToString()))
					{
						throw new ValidationException(Messages.InvalidLogIn);
					}
						
					oResult = dt;
				}
				else
				{
					throw new ValidationException(Messages.InvalidLogIn);
				}
			}
			catch (Exception ex)
			{
				throw CustomException(ex, "Error inesperado al obtener realizar el login.");
			}
		}

	}
}

