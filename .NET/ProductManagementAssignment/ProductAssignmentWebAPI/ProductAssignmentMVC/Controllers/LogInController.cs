using NLog;
using ProductAssignmentMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProductAssignmentMVC.Controllers
{
    public class LogInController : Controller
    {
        public static Logger logObject = LogManager.GetLogger("productAssignmentLogger");

        [HttpGet]
        public ActionResult LogIn()
        {
            try
            {
                //Returns the Login View

                return View();
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(mvcUserDetail formDetails)
        {
            try
            {
                //Takes the Details inserted by user into a formDetails Object of type mvcUserDetail which is a reference copy of UserDetail model class
                //for entities storing user data.

                IQueryable<mvcUserDetail> dbDetails;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("UserDetails").Result;
                dbDetails = response.Content.ReadAsAsync<IEnumerable<mvcUserDetail>>().Result.AsQueryable();
                IEnumerable<mvcUserDetail> tempData = dbDetails.Where(uDts => uDts.EmailID.Equals(formDetails.EmailID) && uDts.Password.Equals(formDetails.Password));

                //Verifies if any records with the given credentials exist in the database. If found initiates a session and directs the Admin to
                //their Dashboard. If not found redirects user back to the Login View.
                if (tempData.Any())
                {
                    Session["UserName"] = tempData.ElementAt(0).FirstName + " " + tempData.ElementAt(0).LastName;
                    return RedirectToAction("Dashboard", "Dashboard");
                }
                TempData["LogInMessage"] = "Admin login Failed";
                return RedirectToAction("LogIn", "LogIn");
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