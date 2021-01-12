using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductAssignmentMVC.Controllers
{
    public class HomeController : Controller
    {
        public static Logger logObject = LogManager.GetLogger("productAssignmentLogger");

        [HttpGet]
        public ActionResult Index()
        {
            //Returns the Index View
            try
            {
                return View();
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }
    }
}