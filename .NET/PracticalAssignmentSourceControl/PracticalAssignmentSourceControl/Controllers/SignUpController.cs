using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticalAssignmentSourceControl.Controllers
{
    public class SignUpController : Controller
    {
        PracticalDatabaseEntities _context;
        public static Logger logger = LogManager.GetLogger("PracticalAssignmentLoggerRule");

        public SignUpController()
        {
            _context = new PracticalDatabaseEntities();
        }

        public ActionResult SignUp()
        {
            logger.Info("Returning Signup Page");
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignupDetail formDetails)
        {
            logger.Info("User submitted the Signup form");
            try
            {
                if (formDetails.Password.Equals(Request["checkPassword"]) && ModelState.IsValid)
                {
                    //Image Path Setting
                    ImageDetail tempImageObject = new ImageDetail();
                    string Title = Path.GetFileNameWithoutExtension(formDetails.ImageFile.FileName);
                    tempImageObject.Title = Title;
                    string Extension = Path.GetExtension(formDetails.ImageFile.FileName);
                    Title = Title + DateTime.Now.ToString("yymmssfff") + Extension;
                    tempImageObject.Path = "~/Image/" + Title;
                    Title = Path.Combine(Server.MapPath("~/Image/"), Title);
                    formDetails.ImageFile.SaveAs(Title);


                    logger.Info("Saving Image details in the database");
                    //Image Uploading
                    _context.ImageDetails.Add(tempImageObject);
                    _context.SaveChanges();

                    logger.Info("Saving the signup details in the database");
                    //Signup Details Uploading
                    formDetails.ImageId = tempImageObject.ImageId;
                    _context.SignupDetails.Add(formDetails);
                    _context.SaveChanges();

                    logger.Info("Saving the log in details in the database");
                    //Login Details uploading
                    LoginDetail tempLoginObject = new LoginDetail();
                    tempLoginObject.EmailId = formDetails.EmailId;
                    tempLoginObject.Password = formDetails.Password;

                    _context.LoginDetails.Add(tempLoginObject);
                    _context.SaveChanges();

                    logger.Info("Redirecting user to login page");

                    TempData["Message"] = "Signed Up Successfully";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Message"] = "Please Check Data Again !!!!";
                return View("SignUp");
            }
            catch(Exception e)
            {
                logger.Error("Caught an Exception. Redirecting to Error Page"+e.Message);
                return RedirectToAction("ErrorPage", "Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}