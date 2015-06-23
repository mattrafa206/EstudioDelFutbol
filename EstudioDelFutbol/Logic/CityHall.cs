using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using GeoGuardianCities.Data.ADONETDataAccess;
using GeoGuardianCities.Logic;
using GeoGuardianCities.Logic.BaseClass;
using GeoGuardianCities.Logic.Utils;
using GeoGuardianCities.Common;
using System.IO;
using GeoGuardianCities.Logic.Resources;

namespace GeoGuardianCities.Logic
{
    public class CityHall : BaseClass.BaseClass
    {
        public CityHall(BizServer bizSvr)
            : base(bizSvr, Assembly.GetCallingAssembly().GetName().Name, true)
        {
        }

        public CityHall(BizServer bizSvr, bool createDBConnection)
            : base(bizSvr, Assembly.GetCallingAssembly().GetName().Name, createDBConnection)
        {
        }

        public CityHall(BizServer bizSvr, DataAccess datAcc)
            : base(bizSvr, datAcc)
        {
        }

        /// <summary>
        /// GetByCode
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="oResult"></param>
        public void GetByCode(string cityHallCode, ref object oResult)
        {
            string strSQL = "";

            try
            {
                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("Code", cityHallCode);

                strSQL = "SELECT * FROM CityHall WITH(NOLOCK) WHERE Code=?";

                CoreUtils.ExcecuteSQL(oDataAccess, strSQL, ref oResult);
            }
            catch (Exception ex)
            {
                throw CustomException(ex, "Error inesperado al obtener el municipio.");
            }
        }

				public void GetCityHalls(ref object oResult)
				{
					string strSQL = "";

					try
					{
						oDataAccess.ClearParameters();

						strSQL = "SELECT CityHall.[CityHallID], CityHall.[CityHallName] FROM CityHall WITH(NOLOCK) INNER JOIN City WITH(NOLOCK) ON CityHall.CityID = City.CityID";

						oResult = oDataAccess.GetDataTable(strSQL);

					}
					catch (Exception ex)
					{
						throw CustomException(ex, "Error inesperado al obtener el listado de municipios.");
					}
				}

				public int GetCityByCityHallID(int cityHallID)
				{
					string strSQL = "";

					try
					{
						DataTable dt = new DataTable();
						oDataAccess.ClearParameters();
						oDataAccess.AddParameter("CityHallID", cityHallID);

						strSQL = "SELECT * FROM CityHall WITH(NOLOCK) WHERE CityHall.CityHallID=?";

						dt = oDataAccess.GetDataTable(strSQL);
						return Convert.ToInt32(dt.Rows[0]["CityID"]);

					}
					catch (Exception ex)
					{
						throw CustomException(ex, "Error inesperado al obtener la ciudad del municipio.");
					}
				}

                public string GetNameByCityHallID(int cityHallID)
                {
                    string strSQL = "";

                    try
                    {
                        DataTable dt = new DataTable();
                        oDataAccess.ClearParameters();
                        oDataAccess.AddParameter("CityHallID", cityHallID);

                        strSQL = "SELECT * FROM CityHall WITH(NOLOCK) WHERE CityHall.CityHallID=?";

                        dt = oDataAccess.GetDataTable(strSQL);
                        return dt.Rows[0]["CityHallName"].ToString();

                    }
                    catch (Exception ex)
                    {
                        throw CustomException(ex, "Error inesperado al obtener el nombre del municipio.");
                    }
                }

				public void GetByID(int cityHallID, ref object oResult)
				{
					string strSQL = "";

					try
					{
						oDataAccess.ClearParameters();
						oDataAccess.AddParameter("CityHallID", cityHallID);

						strSQL = "SELECT * FROM CityHall WITH(NOLOCK) WHERE CityHallID=?";

						CoreUtils.ExcecuteSQL(oDataAccess, strSQL, ref oResult);

					}
					catch (Exception ex)
					{
						throw CustomException(ex, "Error inesperado al obtener el nombre del municipio.");
					}
				}

				public void GetCityHallByCode(string cityHallcode, ref object oResult)
				{

					string strSQL = "";

					try
					{

						oDataAccess.ClearParameters();
						oDataAccess.AddParameter("CityHallCode", cityHallcode);

						strSQL = "SELECT * FROM CityHall WITH(NOLOCK) WHERE Code = ? AND IsActive = 1";

						CoreUtils.ExcecuteSQL(oDataAccess, strSQL, ref oResult);
					}
					catch (Exception ex)
					{
						throw CustomException(ex, "Error inesperado al obtener el municipio por codigo.");
					}
				}

        /// <summary>
        /// GetByName
        /// </summary>
        /// <param name="cityHallName"></param>
        /// <param name="oResult"></param>
        public void GetByName(string cityHallName, ref object oResult)
        {
            string strSQL = "";

            try
            {
                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("CityHallName", cityHallName);

                strSQL = "SELECT * FROM CityHall WITH(NOLOCK) WHERE CityHallName=?";

                CoreUtils.ExcecuteSQL(oDataAccess, strSQL, ref oResult);
            }
            catch (Exception ex)
            {
                throw CustomException(ex, "Error inesperado al obtener el municipio.");
            }
        }

        public int GetAll(int? cityHallID, int? pageNum, ref object oResult)
        {
            string strSQL = "";

            try
            {
                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("CityHallID", cityHallID);
                oDataAccess.AddParameter("pageNum", pageNum);
                oDataAccess.AddOutputParameter("rowqty", SqlDbType.Int);

                strSQL = "spGetCityHalls";

                oResult = oDataAccess.GetDataTable(strSQL, CommandType.StoredProcedure);

                return oDataAccess.GetParameterValue<int>("rowqty");
            }
            catch (Exception ex)
            {
                throw CustomException(ex, "Error inesperado al obtener el listado de municipios.");
            }
        }

        public int GetAll(int? cityHallID, int? cityId, int? pageNum, ref object oResult)
        {
            string strSQL = "";

            try
            {
                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("cityHallID", cityHallID);
                oDataAccess.AddParameter("cityID", cityId);
                oDataAccess.AddParameter("pageNum", pageNum);
                oDataAccess.AddOutputParameter("rowqty", SqlDbType.Int);

                strSQL = "spGetCityHalls";

                oResult = oDataAccess.GetDataTable(strSQL, CommandType.StoredProcedure);

                return oDataAccess.GetParameterValue<int>("rowqty");
            }
            catch (Exception ex)
            {
                throw CustomException(ex, "Error inesperado al obtener el listado de municipios.");
            }
        }

        public int Disable(int cityHallID)
        {
            string strSQL = "";

            try
            {
                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("CityHalID", cityHallID);
                strSQL = "UPDATE CityHall SET IsActive = 0 WHERE CityHallID=?";

                var rowsAffected = Convert.ToInt32(oDataAccess.ExecuteNonQuery(strSQL));

                if (rowsAffected != 1)
                {
                    throw new CoreException("No se ha podido actualizar el registro.", 1);
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw CustomException(ex, "Error inesperado al actualizar los datos del municipio.");
            }
        }

        public int Add(string name, string code, int cityID, bool isActive, string ImgToUpload, string ImgToUploadMain, string aboutUs, string welcome)
        {
            string strSQL = "";
            int resultado;

            try
            {
                oDataAccess.BeginTransaction();
                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("CityHallName", name);
                oDataAccess.AddParameter("Code", code);
                oDataAccess.AddParameter("IsActive", isActive);
                oDataAccess.AddParameter("CityID", cityID);
                oDataAccess.AddParameter("AboutUsText", aboutUs);
                oDataAccess.AddParameter("WelcomeText", welcome);

                strSQL = " INSERT INTO CityHall " +
                                 " (CityHallName,Code,CreationDate,IsActive,CityID,AboutUsText,WelcomeText) VALUES " +
                                 " (?,?,GetDate(),?,?,?,?)";

                resultado = Convert.ToInt32(oDataAccess.ExecuteIdentity(strSQL));


                if (resultado == -1)
                {
                    throw new CoreException("No se ha podido insertar el registro.", 1);
                }

                string fileName = code + ".jpg";
								File.Move(Parameter.Get<string>(oBizServer, GeoGuardianCities.Logic.Parameter.Keys.UploadImagesTemporaryDirectory) + "\\" + ImgToUpload, Parameter.Get<string>(oBizServer, Parameter.Keys.UploadImagesCityHallDirectory) + "\\" + fileName);

                fileName = code + "_main.jpg";
								File.Move(Parameter.Get<string>(oBizServer, GeoGuardianCities.Logic.Parameter.Keys.UploadImagesTemporaryDirectory) + "\\" + ImgToUploadMain, Parameter.Get<string>(oBizServer, Parameter.Keys.UploadImagesCityHallDirectory) + "\\" + fileName);

                oDataAccess.CommitTransaction();
                return resultado;
            }
            catch (Exception ex)
            {
                oDataAccess.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public int Update(int cityHallID, string name, int cityID, bool isActive, string ImgToUpload, string ImgToUploadMain, string code, string aboutUs, string welcome)
        {
            string strSQL = "";

            try
            {
                oDataAccess.BeginTransaction();
                oDataAccess.ClearParameters();

                strSQL = "UPDATE CityHall SET ";

                if (name != String.Empty)
                {
                    oDataAccess.AddParameter("CityHallName", name);
                    strSQL += " CityHallName=?,";
                }

                oDataAccess.AddParameter("CityID", cityID);
                strSQL += " CityID=?, ";

                oDataAccess.AddParameter("IsActive", isActive);
                strSQL += " IsActive=?,";

                oDataAccess.AddParameter("AboutUsText", aboutUs);
                strSQL += " AboutUsText=?,";

                oDataAccess.AddParameter("WelcomeText", welcome);
                strSQL += " WelcomeText=?";

                oDataAccess.AddParameter("CityHallID", cityHallID);
                strSQL += " WHERE CityHallID=?";

                var rowsAffected = Convert.ToInt32(oDataAccess.ExecuteNonQuery(strSQL));

                if (rowsAffected != 1)
                {
                    throw new CoreException("No se ha podido actualizar el registro.", 1);
                }

                if (!string.IsNullOrEmpty(ImgToUpload))
                {
                    string fileName = code + ".jpg";
                    File.Delete(Parameter.Get<string>(oBizServer, Parameter.Keys.UploadImagesCityHallDirectory) + "\\" + fileName);
										File.Move(Parameter.Get<string>(oBizServer, GeoGuardianCities.Logic.Parameter.Keys.UploadImagesTemporaryDirectory) + "\\" + ImgToUpload, Parameter.Get<string>(oBizServer, Parameter.Keys.UploadImagesCityHallDirectory) + "\\" + fileName);
                }

                if (!string.IsNullOrEmpty(ImgToUploadMain))
                {
                    string fileName = code + "_main.jpg";
                    File.Delete(Parameter.Get<string>(oBizServer, Parameter.Keys.UploadImagesCityHallDirectory) + "\\" + fileName);
										File.Move(Parameter.Get<string>(oBizServer, GeoGuardianCities.Logic.Parameter.Keys.UploadImagesTemporaryDirectory) + "\\" + ImgToUploadMain, Parameter.Get<string>(oBizServer, Parameter.Keys.UploadImagesCityHallDirectory) + "\\" + fileName);
                }


                oDataAccess.CommitTransaction();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                oDataAccess.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public void GetByName(string partialName, int maxResults, ref object oResult)
        {

            string strSQL = "";

            try
            {
                if (maxResults > Parameter.Get<int>(oBizServer, Parameter.Keys.MaxResultAllowed))
                    throw new ValidationException(Messages.InvalidParameter);

                oDataAccess.ClearParameters();
                oDataAccess.AddParameter("MaxResults", maxResults);
                oDataAccess.AddParameter("CityHallName", "%" + partialName + "%");
                oDataAccess.AddParameter("Code", "%" + partialName + "%");

                strSQL = "SELECT TOP(?) CityHallID, CityHallName, CreationDate, IsActive FROM CityHall WITH(NOLOCK) WHERE CityHallName LIKE ? OR Code LIKE ?";

                CoreUtils.ExcecuteSQL(oDataAccess, strSQL, ref oResult);
            }
            catch (Exception ex)
            {
                throw CustomException(ex, "Error inesperado al buscar municipios por nombre.");
            }
        }

    }
}