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
    public class SignUpController : Controller
    {
        public static Logger logObject = LogManager.GetLogger("productAssignmentLogger");
        [HttpGet]
        public ActionResult SignUp()
        {
            try
            {
                //Returns the SignUp View

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
        public ActionResult SignUp(mvcUserDetail formDetails)
        {
            try
            {
                //Takes the Details inserted by user into a formDetails Object of type mvcUserDetail which is a reference copy of UserDetail model class
                //for entities storing user data. Returns to Index Page on success and back to SignUp Page on failure.

                if (ModelState.IsValid)
                {
                    if (formDetails.Password.Equals(Request["checkPassword"]))
                    {
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("UserDetails", formDetails).Result;
                        TempData["SignUpMessage"] = "Signed Up Successfully";
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["SignUpMessage"] = "Sign Up Failed";
                    return RedirectToAction("SignUp", "SignUp");
                }
                TempData["SignUpMessage"] = "Sign Up Failed";
                return RedirectToAction("SignUp", "SignUp");
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