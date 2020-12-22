using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticalAssignmentSourceControl.Controllers
{
    public class LogInController : Controller
    {
        public static Logger logger = LogManager.GetLogger("PracticalAssignmentLoggerRule ");

        // GET: LogIn
        public ActionResult LogIn()
        {
            logger.Info("Returning the Login View");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginDetail formDetails)
        {
            try
            {
                logger.Info("User submitted the login form");
                if (ModelState.IsValid)
                {
                    using (PracticalDatabaseEntities _context = new PracticalDatabaseEntities())
                    {
                        var dbObject = _context.LoginDetails.Where(p => p.EmailId.Equals(formDetails.EmailId) && p.Password.Equals(formDetails.Password)).FirstOrDefault();
                        if (dbObject != null)
                        {
                            Session["EmailId"] = formDetails.EmailId;
                            logger.Info("Login succeeded. Redirecting user to Dasboard");
                            return RedirectToAction("Dasboard", "Dashboard");
                        }
                        else
                        {
                            logger.Info("Login Failed. Redirecting user to Login Page");
                            return RedirectToAction("LogIn", "LogIn");
                        }
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                logger.Error("Exception caught. Returning to Error page" + e.Message);
                return RedirectToAction("ErrorPage", "Error");
            }
        }
    }
}