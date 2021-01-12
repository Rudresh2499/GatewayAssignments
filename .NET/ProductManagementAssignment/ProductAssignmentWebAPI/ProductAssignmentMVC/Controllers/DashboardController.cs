using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductAssignmentMVC.Controllers
{
    public class DashboardController : Controller
    {
        public static Logger logObject = LogManager.GetLogger("productAssignmentLogger");

        [HttpGet]
        public ActionResult Dashboard()
        {
            //Returns the Dashboard view of the user if login is successful
            try
            {
                if (Session["UserName"] != null)
                {
                    return View();
                }
                return RedirectToAction("LogIn", "LogIn");
            }
            catch(Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //Invalidates the session and redirects user to the Index Page.
            try
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
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