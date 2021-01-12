using Newtonsoft.Json;
using NLog;
using ProductAssignmentMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProductAssignmentMVC.Controllers
{
    public class ProductController : Controller
    {
        public static Logger logObject = LogManager.GetLogger("productAssignmentLogger");

        [HttpGet]
        public ActionResult AddProduct()
        {
            try
            {
                //Checks if a valid session is initialized and if so then returns the view from where the user can add a new product to the list.
                if (Session["UserName"] != null)
                {
                    //A list containing all the product categories is used to populate the dropdown list for user to select the product Category.

                    IEnumerable<mvcProductCategory> categoryDetails;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductCategories").Result;
                    categoryDetails = response.Content.ReadAsAsync<IEnumerable<mvcProductCategory>>().Result;
                    ViewData["categoryDetails"] = categoryDetails;
                    return View();
                }
                return RedirectToAction("LogIn", "LogIn");
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpPost]
        public ActionResult AddProduct(mvcProductDetail formDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response;

                    //Setting up Title and Path for Small Image to be stored in the database and saves it in the folder Image/Small on the server.
                    mvcSmallImageDetail tempSmallImageObject = new mvcSmallImageDetail();
                    string smallTitle = Path.GetFileNameWithoutExtension(formDetails.SmallImageFile.FileName);
                    tempSmallImageObject.Title = smallTitle;
                    string smallExtension = Path.GetExtension(formDetails.SmallImageFile.FileName);
                    smallTitle = smallTitle + DateTime.Now.ToString("yymmssfff") + smallExtension;
                    tempSmallImageObject.Path = "~/Image/Small/" + smallTitle;
                    smallTitle = Path.Combine(Server.MapPath("~/Image/Small/"), smallTitle);
                    formDetails.SmallImageFile.SaveAs(smallTitle);

                    //Stores the data regarding the small image into the database table for small image.

                    response = GlobalVariables.WebApiClient.PostAsJsonAsync("SmallImageDetails", tempSmallImageObject).Result;

                    //Getting ID of the image stored to be entered into the formDetails.
                    IQueryable<mvcSmallImageDetail> smallImageListObject;
                    response = GlobalVariables.WebApiClient.GetAsync("SmallImageDetails").Result;
                    smallImageListObject = response.Content.ReadAsAsync<IEnumerable<mvcSmallImageDetail>>().Result.AsQueryable();
                    IEnumerable<mvcSmallImageDetail> sIdGetObject = smallImageListObject.Where(sILO => sILO.Path.Equals(tempSmallImageObject.Path));
                    formDetails.SmallImageId = sIdGetObject.ElementAt(0).Id;

                    if (formDetails.LargeImageFile != null)
                    {
                        //Setting up the Path for the Large image to be stored in the database and saves it in the folder Image/Large on the server.
                        mvcLargeImageDetail tempLargeImageObject = new mvcLargeImageDetail();
                        string largeTitle = Path.GetFileNameWithoutExtension(formDetails.LargeImageFile.FileName);
                        tempLargeImageObject.Title = largeTitle;
                        string largeExtension = Path.GetExtension(formDetails.LargeImageFile.FileName);
                        largeTitle = largeTitle + DateTime.Now.ToString("yymmssfff") + largeExtension;
                        tempLargeImageObject.Path = "~/Image/Large/" + largeTitle;
                        largeTitle = Path.Combine(Server.MapPath("~/Image/Large/"), largeTitle);
                        formDetails.LargeImageFile.SaveAs(largeTitle);

                        //Stores the data regarding the large image into the database table for large image.

                        response = GlobalVariables.WebApiClient.PostAsJsonAsync("LargeImageDetails", tempLargeImageObject).Result;

                        //Getting the ID of the Large image to be entered into the formDetails.
                        IQueryable<mvcLargeImageDetail> largeImageListObject;
                        response = GlobalVariables.WebApiClient.GetAsync("LargeImageDetails").Result;
                        largeImageListObject = response.Content.ReadAsAsync<IEnumerable<mvcLargeImageDetail>>().Result.AsQueryable();
                        IEnumerable<mvcLargeImageDetail> lIdGetObject = largeImageListObject.Where(lILO => lILO.Path.Equals(tempLargeImageObject.Path));
                        formDetails.LargeImageId = lIdGetObject.ElementAt(0).Id;
                    }

                    //Fills up the details regarding the category ID and Quantity obtained from the request list to the formDetails object and
                    //finally stores the obtained data into the database.

                    //Redirects to the view showing all the products is successfully uploaded or returns to the same empty page if the operation
                    //fails.

                    formDetails.CategoryId = Int32.Parse(Request["categoryId"]);
                    formDetails.Quantity = Int32.Parse(Request["Quantity"]);
                    response = GlobalVariables.WebApiClient.PostAsJsonAsync("ProductDetails", formDetails).Result;
                    TempData["addedProductName"] = formDetails.Name;
                    return RedirectToAction("ViewAllProducts", "Product");
                }
                return RedirectToAction("AddProduct", "Product");
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            try
            {
                //Returns the View that allows the user to edit the details of the product, which he has selected. The view is populated with data
                //obtained by making a request to the Web API to return the data of the product selected by the user.

                if (Session["UserName"] != null)
                {
                    //A list containing all the product categories is used to populate the dropdown list for user to select the product Category.

                    IEnumerable<mvcProductCategory> categoryDetails;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductCategories").Result;
                    categoryDetails = response.Content.ReadAsAsync<IEnumerable<mvcProductCategory>>().Result;
                    ViewData["categoryDetails"] = categoryDetails;
                    response = GlobalVariables.WebApiClient.GetAsync("ProductDetails/" + id.ToString()).Result;
                    mvcProductDetail tempDetail = response.Content.ReadAsAsync<mvcProductDetail>().Result;
                    var largeImageId = tempDetail.LargeImageId;
                    if (largeImageId != null)
                    {
                        //Stores the path of the large image if uploaded into the ViewBag for accessing it at the View

                        HttpResponseMessage imageResponse = GlobalVariables.WebApiClient.GetAsync("LargeImageDetails/" + largeImageId.ToString()).Result;
                        mvcLargeImageDetail tempImageDetail = imageResponse.Content.ReadAsAsync<mvcLargeImageDetail>().Result;
                        ViewBag.LargeImagePath = tempImageDetail.Path;
                    }
                    return View(tempDetail);
                }
                return RedirectToAction("LogIn", "LogIn");
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(int id, mvcProductDetail formDetails)
        {
            try
            {
                //Removes Small image file from the list of required variables as user may choose to not upload another Small Image which would render
                //the model invalid.
                //The details of the product in its previous state is obtained by making a request to the Web API and stored in previousState object.

                ModelState.Remove("SmallImageFile");
                HttpResponseMessage previousResponse = GlobalVariables.WebApiClient.GetAsync("ProductDetails/" + id.ToString()).Result;
                mvcProductDetail previousState = previousResponse.Content.ReadAsAsync<mvcProductDetail>().Result;
                formDetails.SmallImageId = previousState.SmallImageId;
                if (formDetails.SmallImageFile != null)
                {
                    //If user chooses to upload another small image then the previous image is deleted from the database and new details are
                    //PUT in the database using Web API request. Also teh new image is saved at the folder Image/Small on the server.

                    if (System.IO.File.Exists(Server.MapPath(previousState.SmallImageDetail.Path)))
                    {
                        System.IO.File.Delete(Server.MapPath(previousState.SmallImageDetail.Path));
                    }
                    mvcSmallImageDetail tempSmallImageDetail = new mvcSmallImageDetail();
                    tempSmallImageDetail.Id = formDetails.SmallImageId;
                    string tempTitle = Path.GetFileNameWithoutExtension(formDetails.SmallImageFile.FileName);
                    tempSmallImageDetail.Title = tempTitle;
                    string tempExtension = Path.GetExtension(formDetails.SmallImageFile.FileName);
                    tempTitle = tempTitle + DateTime.Now.ToString("yymmssfff") + tempExtension;
                    tempSmallImageDetail.Path = "~/Image/Small/" + tempTitle;
                    tempTitle = Path.Combine(Server.MapPath("~/Image/Small/"), tempTitle);
                    formDetails.SmallImageFile.SaveAs(tempTitle);

                    HttpResponseMessage actualResponse = GlobalVariables.WebApiClient.PutAsJsonAsync("SmallImageDetails/" + formDetails.SmallImageId.ToString(), tempSmallImageDetail).Result;
                }
                if (formDetails.LargeImageFile != null)
                {
                    //If the user chooses to upload the large image file then there are two paths.

                    mvcLargeImageDetail tempLargeImageDetail = new mvcLargeImageDetail();
                    if (previousState.LargeImageId != null)
                    {
                        //If the user had uploaded a large image previously then the previous image is deleted from the database and the server
                        //and new image is stored at Image/Large on the server and the details are PUT into the LargeImageDetails table at the ID
                        //of the previous image.

                        mvcLargeImageDetail fileToBeDeleted = GlobalVariables.WebApiClient.GetAsync("LargeImageDetails/" + previousState.LargeImageId.ToString()).Result.Content.ReadAsAsync<mvcLargeImageDetail>().Result;
                        if (System.IO.File.Exists(Server.MapPath(fileToBeDeleted.Path)))
                        {
                            System.IO.File.Delete(Server.MapPath(fileToBeDeleted.Path));
                        }
                        tempLargeImageDetail.Id = (int)previousState.LargeImageId;
                        string tempTitle = Path.GetFileNameWithoutExtension(formDetails.LargeImageFile.FileName);
                        tempLargeImageDetail.Title = tempTitle;
                        string tempExtension = Path.GetExtension(formDetails.LargeImageFile.FileName);
                        tempTitle = tempTitle + DateTime.Now.ToString("yymmssfff") + tempExtension;
                        tempLargeImageDetail.Path = "~/Image/Large/" + tempTitle;
                        tempTitle = Path.Combine(Server.MapPath("~/Image/Large/"), tempTitle);
                        formDetails.LargeImageFile.SaveAs(tempTitle);

                        HttpResponseMessage actualResponse = GlobalVariables.WebApiClient.PutAsJsonAsync("LargeImageDetails/" + previousState.LargeImageId.ToString(), tempLargeImageDetail).Result;

                        formDetails.LargeImageId = previousState.LargeImageId;
                    }
                    else
                    {
                        //If user had not uploaded a large image previously then a new entry for this uploaded image is created in the database
                        //and the image is stored at Image/Large.
                        string tempTitle = Path.GetFileNameWithoutExtension(formDetails.LargeImageFile.FileName);
                        tempLargeImageDetail.Title = tempTitle;
                        string tempExtension = Path.GetExtension(formDetails.LargeImageFile.FileName);
                        tempTitle = tempTitle + DateTime.Now.ToString("yymmssfff") + tempExtension;
                        tempLargeImageDetail.Path = "~/Image/Large/" + tempTitle;
                        tempTitle = Path.Combine(Server.MapPath("~/Image/Large/"), tempTitle);
                        formDetails.LargeImageFile.SaveAs(tempTitle);

                        HttpResponseMessage actualResponse = GlobalVariables.WebApiClient.PostAsJsonAsync("LargeImageDetails", tempLargeImageDetail).Result;

                        IQueryable<mvcLargeImageDetail> largeImageListObject;
                        actualResponse = GlobalVariables.WebApiClient.GetAsync("LargeImageDetails").Result;
                        largeImageListObject = actualResponse.Content.ReadAsAsync<IEnumerable<mvcLargeImageDetail>>().Result.AsQueryable();
                        IEnumerable<mvcLargeImageDetail> lIdGetObject = largeImageListObject.Where(lILO => lILO.Path.Equals(tempLargeImageDetail.Path));
                        formDetails.LargeImageId = lIdGetObject.ElementAt(0).Id;
                    }
                }
                if (ModelState.IsValid)
                {
                    //State of the model is checked and if found valid the edited data is PUT into the database using the Web API request. The user
                    //is redirected to the view showing all the product. If ModelState is not valid then the user is redirected to the same page 
                    //with data of the previous state.

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("ProductDetails/" + id.ToString(), formDetails).Result;
                    TempData["editedProductName"] = formDetails.Name;
                    return RedirectToAction("ViewAllProducts", "Product");
                }
                return RedirectToAction("EditProduct", "Product", id);
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult ViewAllProducts()
        {
            try
            {
                //Checks for a valid initialized session and returns the view showing all the product entered into the database, in a tabular
                //format. ALso allows the user to Edit, Delete or get a detailed view of any specific product.
                if (Session["UserName"] != null)
                {
                    mvcProductDetail[] productList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductDetails").Result;
                    var msg = response.Content.ReadAsStringAsync().Result;
                    productList = JsonConvert.DeserializeObject<mvcProductDetail[]>(msg);
                    return View(productList);
                }
                return RedirectToAction("LogIn", "LogIn");
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult ViewProduct(int id)
        { 
            try
            {
                //Checks for a valid initialized session and if found, returns the user with a view that allows them to see the decription of a
                //the selected product in detail.
                if (Session["UserName"] != null)
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductDetails/" + id.ToString()).Result;
                    mvcProductDetail tempDetails = response.Content.ReadAsAsync<mvcProductDetail>().Result;
                    if (tempDetails.LargeImageId != null)
                    {
                        //If a large image has been uploaded by the user then its path is loaded in a ViewBag to load the image in the view.

                        HttpResponseMessage imageResponse = GlobalVariables.WebApiClient.GetAsync("LargeImageDetails/" + tempDetails.LargeImageId.ToString()).Result;
                        mvcLargeImageDetail imageDetails = imageResponse.Content.ReadAsAsync<mvcLargeImageDetail>().Result;
                        ViewBag.LargeImagePath = imageDetails.Path;
                    }
                    return View(tempDetails);
                }
                return RedirectToAction("LogIn", "LogIn");
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            
            try
            {
                //Checks if a valid session is initialized and if found it proceeds to delete the data from database.
                if (Session["UserName"] != null)
                {
                    //Loads the current data from the database into a temp object to load small image detail and large image detail.

                    mvcProductDetail tempObject = new mvcProductDetail();
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductDetails/" + id.ToString()).Result;
                    tempObject = response.Content.ReadAsAsync<mvcProductDetail>().Result;
                    response = GlobalVariables.WebApiClient.DeleteAsync("ProductDetails/" + id.ToString()).Result;
                    if (tempObject.SmallImageId != null)
                    {
                        //Deletes the small image file from server and then from the database.

                        string tempPath = tempObject.SmallImageDetail.Path;
                        if (System.IO.File.Exists(Server.MapPath(tempPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(tempPath));
                        }
                        response = GlobalVariables.WebApiClient.DeleteAsync("SmallImageDetails/" + tempObject.SmallImageId.ToString()).Result;
                    }
                    if (tempObject.LargeImageId != null)
                    {
                        //Deletes the large image from the server and then from the database if the user has uploaded it.
                        int tempLargeId = (int)tempObject.LargeImageId;
                        mvcLargeImageDetail tempLargeImageObject = new mvcLargeImageDetail();
                        response = GlobalVariables.WebApiClient.GetAsync("LargeImageDetails/" + tempLargeId.ToString()).Result;
                        tempLargeImageObject = response.Content.ReadAsAsync<mvcLargeImageDetail>().Result;
                        if (System.IO.File.Exists(Server.MapPath(tempLargeImageObject.Path)))
                        {
                            System.IO.File.Delete(Server.MapPath(tempLargeImageObject.Path));
                        }
                        response = GlobalVariables.WebApiClient.DeleteAsync("LargeImageDetails/" + tempLargeId.ToString()).Result;
                    }
                    TempData["deletedProductName"] = tempObject.Name;
                    return RedirectToAction("ViewAllProducts", "Product");
                }
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