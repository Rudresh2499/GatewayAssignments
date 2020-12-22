using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticalAssignmentSourceControl.Controllers
{
    public class DashboardController : Controller
    {
        public static Logger logger = LogManager.GetLogger("PracticalAssignmentLoggerRule");

        // GET: Dashboard
        public ActionResult Dasboard()
        {
            logger.Info("Verifying session");

            if(Session["EmailId"] != null)
            {
                logger.Info("Session Verified");
                return View();
            }
            else
            {
                logger.Info("Session not Verified");
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult Logout()
        {
            logger.Info("User logging out and terminating the session");
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}