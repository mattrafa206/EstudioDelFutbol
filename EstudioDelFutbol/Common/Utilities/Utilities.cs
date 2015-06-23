using System;
using System.IO;
using System.Web;
using System.Net;
using System.Xml;
using System.Text;
using System.Data;
using Microsoft.Win32;
using System.Net.Mail;
using System.Xml.XPath;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Types;
using System.Collections.Specialized;

namespace EstudioDelFutbol.Common.Utils
{
    public static class Utilities
    {
        /// <summary>
        /// Devuelve el path de la Aplicacion.
        /// </summary>
        /// <returns>Path de la Aplicacion.</returns>
        public static String AppPath()
        {
            return Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        }

        /// <summary>
        /// Obtiene el Nombre del Dia seg�n la fecha ingresada.
        /// </summary>
        /// <param name="fecha">Fecha a obtener el Nombre del D�a.</param>
        /// <returns>Nombre del D�a.</returns>
        public static string GetDayName(DateTime fecha, CultureInfo culture)
        {
            string dayName = "";

            try
            {
                // Defino Cultura.
                // CultureInfo culture = new CultureInfo("es-ES");
                Calendar ca = culture.Calendar;

                // Obtengo Nombre de d�a.
                dayName = culture.DateTimeFormat.DayNames[Convert.ToInt32(fecha.DayOfWeek)];

                // Pongo en May�scula la primer letra.
                dayName = dayName.Substring(0, 1).ToUpper() + dayName.Substring(1, dayName.Length - 1);
                return dayName;
            }
            catch (Exception)
            {
                return fecha.DayOfWeek.ToString();
            }
        }

        /// <summary>
        /// GetMonthName
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string GetMonthName(DateTime fecha, CultureInfo culture)
        {
            string MonthName = "";

            try
            {
                // Defino Cultura.
                // CultureInfo culture = new CultureInfo("es-ES");
                Calendar ca = culture.Calendar;

                // Obtengo Nombre de d�a.
                MonthName = culture.DateTimeFormat.MonthNames[fecha.Month - 1];

                // Pongo en May�scula la primer letra.
                MonthName = MonthName.Substring(0, 1).ToUpper() + MonthName.Substring(1, MonthName.Length - 1);
                return MonthName;
            }
            catch (Exception)
            {
                return fecha.Month.ToString();
            }
        }

        /// <summary>
        /// FormatDate
        /// </summary>
        /// <param name="date">dd/mm/yyyy</param>
        /// <returns></returns>
        public static DateTime FormatDate(String date)
        {

            string[] Date = date.Split('/');

            if (Date.Length != 3)
                throw new Exception(date + " Formato incorrecto");

            return new DateTime(Convert.ToInt32(Date[2]), Convert.ToInt32(Date[1]), Convert.ToInt32(Date[0]));
        }

        /// <summary>
        /// FormatDateTime
        /// </summary>
        /// <param name="date">dd/mm/yyyy HH:mm:ss</param>
        /// <returns></returns>
        public static DateTime FormatDateTime(String date)
        {

            string[] DatePart = date.Split(' ');

            if (DatePart.Length != 2)
                throw new Exception(date + " Formato incorrecto");

            string[] Date = DatePart[0].Split('/');


            if (Date.Length != 3)
                throw new Exception(date + " Formato incorrecto");

            string[] Time = DatePart[1].Split(':');

            if (Time.Length != 3)
                throw new Exception(date + " Formato incorrecto");


            return new DateTime(Convert.ToInt32(Date[2]), Convert.ToInt32(Date[1]), Convert.ToInt32(Date[0]), Convert.ToInt32(Time[0]), Convert.ToInt32(Time[1]), Convert.ToInt32(Time[2]));
        }

        /// <summary>
        /// FormatDateTime
        /// </summary>
        /// <param name="date">dd/MM/yyyy HH:mm:ssGMT(-/+)</param>
        /// <param name="serverTimeZone"></param>
        /// <returns></returns>
        public static DateTime FormatDateTime(String date, int serverTimeZone)
        {
            int clientTimeZone = Convert.ToInt16(date.Substring(19));
            return FormatDateTime(date.Substring(0, 19)).AddHours(serverTimeZone - clientTimeZone);
        }

        public static string GetDateTimeZone(DateTime fecha, int timeZone)
        {
            if (timeZone < 0)
                return fecha.ToString("dd/MM/yyyy HH:mm:ss") + timeZone.ToString();
            else
                return fecha.ToString("dd/MM/yyyy HH:mm:ss") + "+" + timeZone.ToString();
        }

        public static string FormatClientDateTime(DateTime dateTime, int clientTimeZone, int serverTimeZone)
        {
            string dateTimeZone = GetDateTimeZone(dateTime, clientTimeZone);
            DateTime formatDateTime = FormatDateTime(dateTimeZone.Substring(0, 19)).AddHours(clientTimeZone - serverTimeZone);
            return formatDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Devuelde el IPAdress del server actual.
        /// </summary>
        /// <returns>IPAdress del server actual.</returns>
        public static IPAddress[] GetServerIPAddress()
        {
            try
            {
                string strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                return addr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guarda en un archivo el texto ingresado.
        /// </summary>
        /// <param name="textToSave">Texto a guardar en el archivo.</param>
        /// <param name="path">Ruta a guardar el archivo.</param>
        /// <param name="fileName">Nombre del Archivo. Si no se establece, se genera uno random.</param>
        /// <returns>DataTable con informacion del Nombre del Archivo y el tama�o.</returns>
        public static DataTable SaveToDisk(string textToSave, string path, string fileName)
        {
            try
            {
                return SaveToDisk(textToSave, path, fileName, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guarda (o agrega) en un archivo el texto ingresado.
        /// </summary>
        /// <param name="textToSave">Texto a guardar en el archivo.</param>
        /// <param name="path">Ruta a guardar el archivo.</param>
        /// <param name="fileName">Nombre del Archivo. Si no se establece, se genera uno random.</param>
        /// <param name="append">TRUE->Agrega datos al archivo si existe. FALSE-> Reemplaza el archivo existente con la nueva info.</param>
        /// <returns>DataTable con informacion del Nombre del Archivo y el tama�o.</returns>
        public static DataTable SaveToDisk(string textToSave, string path, string fileName, bool append)
        {
            Random rnd = null;
            StreamWriter sw = null;
            byte[] info = null;

            DataTable dtResult;

            try
            {
                dtResult = new DataTable();
                dtResult.Columns.Add("FileID");
                dtResult.Columns.Add("FileSize");

                rnd = new Random(DateTime.Now.Millisecond);

                if (fileName.Trim() == "")
                {
                    fileName = "FILE_" + rnd.Next(1, 900000000).ToString("000000000") + ".TXT";
                }

                // Si existe el archivo, lo borro.
                if (!append && File.Exists(path + fileName))
                {
                    File.Delete(path + fileName);
                }

                sw = new StreamWriter(path + fileName, append, Encoding.Default);
                sw.Write(textToSave);
                sw.Close();
                sw.Dispose();
                sw = null;

                info = new System.Text.UTF8Encoding().GetBytes(textToSave);
                dtResult.Rows.Add(fileName, info.Length);

                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar el Archivo (" + path + fileName + ")", ex);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
            }
        }

        /// <summary>
        /// Realiza el env�o de correos
        /// </summary>
        /// <param name="mailAddress">Direcciones de correo destino (separados por coma)</param>
        /// <param name="CC">Direcciones de correo destino que se ubicar�n en el campo "con copia" (separados por coma)</param>
        /// <param name="BCC">Direcciones de correo destino que se ubicar�n en el campo "con copia oculta" (separados por coma)</param>
        /// <param name="mailSubject">Asunto.</param>
        /// <param name="mailFrom">Mail Origen</param>
        /// <param name="mailBody">Cuerpo del Mensaje</param>
        /// <param name="lstAtt">Lista de datos adjuntos</param>
        /// <param name="usingPort">Determina si el env�o ser� sincr�nico o no</param>
        /// <param name="oBizSvr">Objeto bizServer.</param>
        public static void SendMail(string mailAddress, string CC, string BCC, string mailSubject, string mailFrom,
                                    string mailBody, List<Attachment> lstAtt, bool usingPort, BizServer oBizSvr)
        {
            MailMessage message = null;
            SmtpClient smtp = null;

            try
            {
                message = new MailMessage();
                if (mailAddress.Trim() != "")
                {
                    message.To.Add(mailAddress.Replace(";", ","));
                }
                message.Subject = mailSubject;
                message.From = new MailAddress(mailFrom);
                if (CC != "")
                {
                    message.CC.Add(CC.Trim().Replace(";", ","));
                }
                if (BCC != "")
                {
                    message.Bcc.Add(BCC.Trim().Replace(";", ","));
                }
                message.Body = mailBody;
                message.IsBodyHtml = true;

                smtp = new SmtpClient();
                if (usingPort)
                {
                    string smtpServer = ""; //Parametro.GetValorParametro(oBizSvr.Parametros, "SMTP_Server");
                    string smtpPort = ""; //Parametro.GetValorParametro(oBizSvr.Parametros, "SMTP_Server_Port");
                    string smtpUserName = ""; //Parametro.GetValorParametro(oBizSvr.Parametros, "SMTP_UserName");
                    string smtpUserPassword = ""; //Parametro.GetValorParametro(oBizSvr.Parametros, "SMTP_UserPassword");

                    if (lstAtt.Count > 0)
                    {
                        foreach (Attachment att in lstAtt)
                        {
                            message.Attachments.Add(att);
                        }
                    }

                    smtp.Host = smtpServer;
                    smtp.Port = Convert.ToInt32(smtpPort);
                    smtp.Credentials = new NetworkCredential(smtpUserName, smtpUserPassword);
                }
                else
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = ""; //Parametro.GetValorParametro(oBizSvr.Parametros, "SMTP_ServerPickupDirectory");
                }
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (message != null)
                {
                    if (message.Attachments.Count > 0)
                    {
                        for (int i = 0; i < message.Attachments.Count; i++)
                        {
                            message.Attachments[i].Dispose();
                            message.Attachments.Remove(message.Attachments[i]);
                            i--;
                        }
                    }

                    message.Attachments.Clear();
                    message.Attachments.Dispose();
                    message.Dispose();
                    message = null;
                }

                if (smtp != null)
                {
                    smtp = null;
                }
            }
        }

        /// <summary>
        /// Realiza el env�o de Mails
        /// </summary>
        /// <param name="mailAddress">Mails Destinos (separados por coma.)</param>
        /// <param name="mailSubject">Asunto.</param>
        /// <param name="mailFrom">Mail Origen</param>
        /// <param name="mailBody">Cuerpo del Mensaje</param>
        /// <param name="oBizSvr">Objeto bizServer.</param>
        public static void SendMail(string mailAddress, string mailSubject, string mailFrom,
                                    string mailBody, BizServer oBizSvr)
        {
            try
            {
                SendMail(mailAddress, "", "", mailSubject, mailFrom, mailBody, new List<Attachment>(), true, oBizSvr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza el env�o de Mails
        /// </summary>
        /// <param name="mailAddress">Mails Destinos (separados por coma.)</param>
        /// <param name="mailSubject">Asunto.</param>
        /// <param name="mailFrom">Mail Origen</param>
        /// <param name="mailBody">Cuerpo del Mensaje</param>
        /// <param name="lstAtt">Lista de datos adjuntos</param>
        /// <param name="oBizSvr">Objeto bizServer.</param>
        public static void SendMail(string mailAddress, string mailSubject, string mailFrom,
                                    string mailBody, List<Attachment> lstAtt, BizServer oBizSvr)
        {
            try
            {
                SendMail(mailAddress, "", "", mailSubject, mailFrom, mailBody, lstAtt, true, oBizSvr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SendMail
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="smtpHost"></param>
        /// <param name="smtpPort"></param>
        /// <param name="message"></param>
        /// <param name="sendSync"></param>
        public static void SendMail(EstudioDelFutbol.Logger.Log logger, string smtpHost, int smtpPort, string smtpUserName, string smtpPassword, MailMessage message, bool sendSync)
        {
            if (sendSync)
            {
                new SendSmtpMail().SendMail(new SendSmtpMail.SmtpParams(logger, smtpHost, smtpPort, smtpUserName, smtpPassword, message));
            }
            else
            {
                Thread th = new Thread(new SendSmtpMail().SendMail);
                th.Start(new SendSmtpMail.SmtpParams(logger, smtpHost, smtpPort, smtpUserName, smtpPassword, message));
            }
        }

        private class SendSmtpMail
        {
            public class SmtpParams
            {

                public EstudioDelFutbol.Logger.Log Logger { get; set; }
                public string SmtpHost { get; set; }
                public int SmtpPort { get; set; }
                public string SmtpUserName { get; set; }
                public string SmtpPassword { get; set; }
                public MailMessage MailMessage { get; set; }

                public SmtpParams(EstudioDelFutbol.Logger.Log logger, string smtpHost, int smtpPort, string smtpUserName, string smtpPassword, MailMessage mailMessage)
                {
                    Logger = logger;
                    SmtpHost = smtpHost;
                    SmtpPort = smtpPort;
                    SmtpUserName = smtpUserName;
                    SmtpPassword = smtpPassword;
                    MailMessage = mailMessage;
                }
            }

            public void SendMail(object state)
            {
                SmtpParams smtpParams = (SmtpParams)state;
                SmtpClient smtp = null;

                try
                {
                    smtp = new SmtpClient();
                    smtp.Host = smtpParams.SmtpHost;
                    smtp.Port = smtpParams.SmtpPort;
                    //smtp.EnableSsl = true;
                    if (!String.IsNullOrEmpty(smtpParams.SmtpUserName) && !String.IsNullOrEmpty(smtpParams.SmtpPassword))
                        smtp.Credentials = new NetworkCredential(smtpParams.SmtpUserName, smtpParams.SmtpPassword);
                    smtp.Send(smtpParams.MailMessage);
                }
                catch (Exception ex)
                {
                    smtpParams.Logger.TraceError("Send Email Error: " + ex.Message);
                }
                finally
                {
                    if (smtp != null)
                        smtp = null;
                }
            }
        }

        public static Image resizeImage(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Transparent);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static string RemoveAccentsWithRegEx(string inputString)
        {
            Regex replace_a_Accents = new Regex("[�|�|�|�]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[�|�|�|�]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[�|�|�|�]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[�|�|�|�]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[�|�|�|�]", RegexOptions.Compiled);
            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_u_Accents.Replace(inputString, "u");
            return inputString;
        }

        public static double? GetLat(object geographyPoint)
        {
            SqlGeography sqlGeography = null;

            try
            {
                if (geographyPoint == null)
                    throw new ArgumentNullException("geographyPoint");

                sqlGeography = (SqlGeography)geographyPoint;

                if (!sqlGeography.IsNull)
                    return (double)sqlGeography.Lat;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlGeography = null;
            }
        }

        public static double? GetLng(object geographyPoint)
        {
            SqlGeography sqlGeography = null;

            try
            {
                if (geographyPoint == null)
                    throw new ArgumentNullException("geographyPoint");

                sqlGeography = (SqlGeography)geographyPoint;

                if (!sqlGeography.IsNull)
                    return (double)sqlGeography.Long;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlGeography = null;
            }
        }

        public static bool DayOfWeekSelected(int mask, CoreConstants.DaysOfWeek dayOfWeek)
        {
            return (mask & (uint)Math.Pow(2, (uint)dayOfWeek)) != 0;
        }

        public static Image RoundCorners(Image StartImage, int CornerRadius, Color BackgroundColor)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(BackgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(StartImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        public static T ReadAppSettings<T>(NameValueCollection appSettings, string key, T defaultValue)
        {
            object value = appSettings[key];

            if (value == null || value.ToString() == string.Empty)
                return defaultValue;
            else
            {
                int parseInt;
                if (Int32.TryParse(value.ToString(), out parseInt))
                {
                    value = parseInt;
                    return (T)value;
                }
                else
                    return (T)value;
            }
        }

        public static bool ValidatePassword(string password)
        {
            Regex rePassword = new Regex("^(?=.*[0-9])(?=.*[a-zA-Z])^[a-zA-Z0-9@*#-_?�%&!]{6,250}$");

            if (rePassword.IsMatch(password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool ValidateEmail(string email)
        {
            Regex reEmail = new Regex("(['\"]{1,}.+['\"]{1,}\\s+)?<?[\\w\\.\\-]+@[^\\.][\\w\\.\\-]+\\.[a-z]{2,}>?");

            if (reEmail.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /*
         * Convert DataTeable to CSV.
         */
        public static void CreateCSVFile(DataTable dtDataTablesList, string strFilePath)
        {
            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter(strFilePath + "\\"  + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv", false);

            //First we will write the headers.
            int iColCount = dtDataTablesList.Columns.Count;

            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dtDataTablesList.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);

            // Now write all the rows.
            foreach (DataRow dr in dtDataTablesList.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

				public static Color HexToColor(string hexColor)
				{
					//Remove # if present
					if (hexColor.IndexOf('#') != -1)
						hexColor = hexColor.Replace("#", "");

					int red = 0;
					int green = 0;
					int blue = 0;

					if (hexColor.Length == 6)
					{
						//#RRGGBB
						red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
						green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
						blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
					}
					else if (hexColor.Length == 3)
					{
						//#RGB
						red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
						green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
						blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
					}

					return Color.FromArgb(red, green, blue);
				}

    }
}

