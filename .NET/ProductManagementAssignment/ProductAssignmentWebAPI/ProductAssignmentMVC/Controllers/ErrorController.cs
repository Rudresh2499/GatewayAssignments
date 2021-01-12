using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductAssignmentMVC.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Error()
        {
            //Returns user to a Error Page and invalidates the session, when an exception is caught.

            if (Session["UserName"] != null)
            {
                Session.Clear();
            }
            return View();
        }
    }
}