using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Configuration;
using EstudioDelFutbol.Logic;
using EstudioDelFutbol.Common;
using EstudioDelFutbol.Models;
using EstudioDelFutbol.Logger;
using System.Text.RegularExpressions;
using EstudioDelFutbol.Common.Utils;
using EstudioDelFutbol.Logic.Resources;
using System.IO;
using EstudioDelFutbol.CommonWeb;

namespace EstudioDelFutbol.Controllers
{
    public class PagesController : BaseController
    {


        const int lenght100 = 100;
        const int lenght250 = 250;
        const int lenght500 = 500;

        [HttpGet]
        public ActionResult Login()
        {
            if (base.IsAuthenticated)
                return RedirectToAction("Analysis", "Pages");
            else
                return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Analysis()
        {
            if (base.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Login", "Pages");
        }

        public ActionResult Login(EstudioDelFutbol.Models.LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    String sResult = String.Empty;

                    sResult = EstudioDelFutbol.CommonWeb.Security.Authenticate(model.UserName, model.Password, bizServer);

                    if (sResult == String.Empty)
                    {
                        return RedirectToAction("Analysis", "Pages");
                    }
                    else
                    {
                        ModelState.AddModelError("", sResult);
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "");
                base.CustomException(ex, "");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Pages");
        }

        public JsonResult GetOptionsAnalysis()
        {
            List<AnalysisOptionModel> lstAnOp = new List<AnalysisOptionModel>();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string AnalysisPath = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).InternalPath, "Analisis");
                    foreach (string dir in Directory.GetDirectories(AnalysisPath))
                    {
                        AnalysisOptionModel anOp = new AnalysisOptionModel();
                        anOp.opName = dir.Remove(0, AnalysisPath.Length + 1);
                        string[] file = Directory.GetFiles(dir);
                        if (file.Length > 0)
                        {
                            FileInfo info = new FileInfo(file[0]);

                            anOp.fileName = Path.GetFileNameWithoutExtension(info.Name);
                            anOp.fullName = Utils.GetUserInfoFromTicket(User.Identity).ExternalPath + "Analisis/" + anOp.opName + "/" + info.Name;
                            anOp.size = info.Length / 1048576;
                            anOp.date = info.LastWriteTime.ToShortDateString();
                        }
                        lstAnOp.Add(anOp);
                    }
                }
            }
            catch (Exception ex)
            {
                base.CustomException(ex, "");
            }

            return Json(lstAnOp);

        }

        public JsonResult GetOptionsMatches()
        {
            List<MatchesOptionModel> lstMaOp = new List<MatchesOptionModel>();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string MatchesPath = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).InternalPath, "Partidos");
                    foreach (string file in Directory.GetFiles(MatchesPath))
                    {
                        FileInfo info = new FileInfo(file);

                        MatchesOptionModel maOp = new MatchesOptionModel();
                        maOp.opName = Path.GetFileNameWithoutExtension(info.Name);
                        maOp.fileName = Path.GetFileNameWithoutExtension(info.Name);
                        maOp.fullName = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).ExternalPath + "Partidos/", info.Name);
                        maOp.size = info.Length / 1048576;
                        maOp.date = info.LastWriteTime.ToShortDateString();
                        lstMaOp.Add(maOp);
                    }
                }
            }
            catch (Exception ex)
            {
                base.CustomException(ex, "");
            }

            return Json(lstMaOp);

        }

        public JsonResult GetOptionsPlayers()
        {
            List<PlayersOptionModel> lstPlaOp = new List<PlayersOptionModel>();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string PlayersPath = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).InternalPath, "Mejores Jugadores");
                    foreach (string file in Directory.GetFiles(PlayersPath))
                    {
                        FileInfo info = new FileInfo(file);

                        PlayersOptionModel plaOp = new PlayersOptionModel();
                        plaOp.opName = Path.GetFileNameWithoutExtension(info.Name);
                        plaOp.fileName = Path.GetFileNameWithoutExtension(info.Name);
                        plaOp.fullName = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).ExternalPath + "Mejores Jugadores/", info.Name);
                        plaOp.size = info.Length / 1048576;
                        plaOp.date = info.LastWriteTime.ToShortDateString();
                        lstPlaOp.Add(plaOp);
                    }
                }
            }
            catch (Exception ex)
            {
                base.CustomException(ex, "");
            }

            return Json(lstPlaOp);

        }

        public JsonResult GetLastTrainings()
        {

            List<string> lstLastTrainings = new List<string>();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string LastTrainingsPath = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).InternalPath, "Estadisticas\\Ultimas 2 formaciones");
                    foreach (string file in Directory.GetFiles(LastTrainingsPath))
                    {
                        FileInfo info = new FileInfo(file);
                        lstLastTrainings.Add(Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).ExternalPath + "Estadisticas/Ultimas 2 formaciones/", info.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                base.CustomException(ex, "");
            }
            return Json(lstLastTrainings);

        }

        public JsonResult GetLastSystems()
        {

            List<string> lstLastSystems = new List<string>();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string LastSystemsPath = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).InternalPath, "Estadisticas\\Ultimos 2 sistemas");
                    foreach (string file in Directory.GetFiles(LastSystemsPath))
                    {
                        FileInfo info = new FileInfo(file);
                        lstLastSystems.Add(Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).ExternalPath + "Estadisticas/Ultimos 2 sistemas/", info.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                base.CustomException(ex, "");
            }
            return Json(lstLastSystems);
        }

        public JsonResult GetMoreStatistics()
        {

            List<string> lstMoreStatistics = new List<string>();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string LastMoreStatisticsPath = Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).InternalPath, "Estadisticas\\Mas Estadisticas");
                    foreach (string file in Directory.GetFiles(LastMoreStatisticsPath))
                    {
                        FileInfo info = new FileInfo(file);
                        lstMoreStatistics.Add(Path.Combine(Utils.GetUserInfoFromTicket(User.Identity).ExternalPath + "Estadisticas/Mas Estadisticas/", info.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                base.CustomException(ex, "");
            }
            return Json(lstMoreStatistics);
        }

        public ActionResult SendMailContact(string fullName, string email, string subject, string phone, string clubName, string body, string title)
        {
            bool customMsg = true;
         
            try
            {
                //fullName
                if (fullName.Length == 0 || fullName.Length > lenght250)
                {
                    throw new CoreException("El Nombre y Apellido no puede estar vacío o contener mas de " + lenght250 + " caracteres", 1);
                }

                //email
                if (email.Length == 0 || email.Length > 250)
                {
                    throw new CoreException("El Email no puede ser vacío o contener mas de " + lenght250 + " caracteres", 1);
                }

                //ClubName
                if (clubName.Length == 0 || clubName.Length > lenght250)
                {
                    throw new CoreException("El Nombre del Club no puede estar vacío o contener mas de " + lenght250 + " caracteres", 1);
                }

                //phone
                if (phone.Length > 30)
                {
                    throw new CoreException("El Teléfono no puede contener mas de " + 30 + " caracteres", 1);
                }

                //body
                if (body.Length == 0 || body.Length > lenght500)
                {
                    throw new CoreException("La consulta no puede estar vacía o contener mas de " + lenght500 + " caracteres", 1);
                }


                customMsg = false;

                #region Send Contact Email

                MailHelper oMailHelper = new MailHelper(bizServer.Log);
                var smtpHost = ConfigurationManager.AppSettings.Get("SMTP_HOST");
                var smtpPort = Int32.Parse(ConfigurationManager.AppSettings.Get("SMTP_PORT"));
                var smtpUserName = ConfigurationManager.AppSettings.Get("SMTP_USER");
                var smtpPassword = ConfigurationManager.AppSettings.Get("SMTP_PASSWORD");
                
                string mailBody = string.Format(Mails.MailContact, fullName, email, phone, clubName, body);
                oMailHelper.SendMailAccountCreated(smtpHost, smtpPort, smtpUserName, smtpPassword, ConfigurationManager.AppSettings.Get("SMTP_FROM"), ConfigurationManager.AppSettings.Get("SMTP_TO"), "EstudioDelFutbol - " + title, mailBody);

                #endregion


                return Content("Success");
            }
            catch (Exception ex)
            {

                base.CustomException(ex, "");
                return Content((customMsg) ? ex.Message : "Ocurrio el enviar el mail de contacto.");
            }

        }

    }
}
