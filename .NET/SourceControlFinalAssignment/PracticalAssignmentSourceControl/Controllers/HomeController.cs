using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticalAssignmentSourceControl.Controllers
{
    public class HomeController : Controller
    {
        public static Logger logger = LogManager.GetLogger("PracticalAssignmentLoggerRule");
        public ActionResult Index()
        {
            logger.Info("Returning Index view");
            return View();
        }

        public ActionResult About()
        {
            logger.Info("Returning About View");
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            logger.Info("Returning Contact View");
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}