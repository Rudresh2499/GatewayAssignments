using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticalAssignmentSourceControl.Controllers
{
    public class ErrorController : Controller
    {
        public static Logger logger = LogManager.GetLogger("PracticalAssignmentLoggerRule");

        // GET: Error
        public ActionResult ErrorPage()
        {
            logger.Info("Returning the Index view to the user after catching exception");
            return View();
        }
    }
}