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
    public class ProductCategoryController : Controller
    {
        public static Logger logObject = LogManager.GetLogger("productAssignmentLogger");

        [HttpGet]
        public ActionResult AddProductCategory()
        {
            try
            {
                //First checks if a session is initialized. If a session exists then returns the view which allows the user to add new product category

                if (Session["UserName"] != null)
                {
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
        public ActionResult AddProductCategory(mvcProductCategory formDetails)
        {
            try
            {
                //Stores the details about Product Category entered by the user into a object formDetails of type mvcProductCategory which is a copy
                //of ProductCategory model class.
                if (ModelState.IsValid)
                {
                    //Checks if the model state is valid and if so adds the product category to the database and redirects the user to the view showing
                    //all the categories that are in the database. If model state is not valid then it redirects user back to the same view.

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("ProductCategories", formDetails).Result;
                    TempData["CategoryMessage"] = "Category Successfully Added";
                    return RedirectToAction("ViewAllProductsCategory", "ProductCategory");
                }
                TempData["CategoryMessage"] = "Category could not be Added";
                return RedirectToAction("AddProductCategory", "ProductCategory");
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult EditProductCategory(int id)
        {
            try
            {
                //Checks if a valid session exists and if so returns a view where the admin can edit the details of a product category. The view is pre-
                //populated with currently stored values in the database.

                if (Session["UserName"] != null)
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductCategories/" + id.ToString()).Result;
                    return View(response.Content.ReadAsAsync<mvcProductCategory>().Result);
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
        public ActionResult EditProductCategory(int id, mvcProductCategory formDetails)
        {
            try
            {
                //Checks if the model state is valid and if so then uses the PUT method of the API to store the changed values entered by the user into the
                //Database. Redirects to list of all categories if successfully edited else redirects to the same page.

                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("ProductCategories/" + id.ToString(), formDetails).Result;
                    return RedirectToAction("ViewAllProductsCategory", "ProductCategory");
                }
                return RedirectToAction("EditProductCategory", "ProductCategory", id);
            }
            catch (Exception e)
            {
                logObject.Error(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public ActionResult ViewAllProductsCategory()
        {
            try
            {
                //Checks if a valid session is initialized and if so returns the view which displays all the product categories. The view is populated
                //by the list of all the categories retrieved by making an API request. Redirects user to Login page if session is invalid.

                if (Session["UserName"] != null)
                {
                    IEnumerable<mvcProductCategory> categoryList;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductCategories").Result;
                    categoryList = response.Content.ReadAsAsync<IEnumerable<mvcProductCategory>>().Result;
                    return View(categoryList);
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
        public ActionResult DeleteProductCategory(int id)
        {
            try
            {
                //Checks if a valid session is initialized and if so proceeds to delete the category which user wants to delete.

                if (Session["UserName"] != null)
                {
                    //Retrieves an enumerable list of all the products belonging to the category which user wants to delete and proceeds to delete them
                    //from the database.

                    IQueryable<mvcProductDetail> categoryProducts;
                    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("ProductDetails").Result;
                    categoryProducts = response.Content.ReadAsAsync<IEnumerable<mvcProductDetail>>().Result.AsQueryable();
                    IEnumerable<mvcProductDetail> toBeDeletedProducts = categoryProducts.Where(cP => cP.CategoryId.Equals(id));
                    if (toBeDeletedProducts != null)
                    {
                        //If the list is not null then for each product instantiates a context object of the ProductController and call the
                        //delete method on each of the product.

                        foreach (mvcProductDetail item in toBeDeletedProducts)
                        {
                            var tempControlObject = DependencyResolver.Current.GetService<ProductController>();
                            tempControlObject.ControllerContext = new ControllerContext(this.Request.RequestContext, tempControlObject);
                            tempControlObject.DeleteProduct(item.Id);
                        }
                    }

                    //Once all the products are deleted then deletes the entry of the category from the database. Redirects to the view showing list
                    //of all the categories if successful. If session is not found redirects user to the Login view.

                    response = GlobalVariables.WebApiClient.DeleteAsync("ProductCategories/" + id.ToString()).Result;
                    return RedirectToAction("ViewAllProductsCategory", "ProductCategory");
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