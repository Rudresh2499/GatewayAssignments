using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment_1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.SignUpModel applicant)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(applicant);
        }
    }
}