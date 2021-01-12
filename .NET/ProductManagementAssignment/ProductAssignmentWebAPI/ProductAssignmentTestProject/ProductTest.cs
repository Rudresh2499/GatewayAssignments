using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ProductAssignmentMVC.Models;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace ProductAssignmentTestProject
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetAllProductsTest_ShouldReturnAllProducts()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44316/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            HttpResponseMessage response = WebApiClient.GetAsync("ProductDetails").Result;
            var ProductList = response.Content.ReadAsAsync<IEnumerable<mvcProductDetail>>().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, ProductList.Count());
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void GetOneProduct_ShouldReturnOnlyOneProduct()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44316/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            HttpResponseMessage response = WebApiClient.GetAsync("ProductDetails/9").Result;
            var ProductFound = response.Content.ReadAsAsync<mvcProductDetail>().Result;

            //Assert
            Assert.AreEqual("Redgear A-15", ProductFound.Name);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void EditProductDetails_ShouldReturnBadequest()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44316/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            mvcProductDetail testDetails = new mvcProductDetail()
            {
                Name = "Test Details",
                Quantity = 5,
                ShortDescription = "Entering Test details that should fail.",
                SmallImageId = 1009,
            };

            //Act
            HttpResponseMessage response = WebApiClient.PutAsJsonAsync("ProductDetails/9", testDetails).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void EditProductDetails_ShouldReturnNotFound()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44316/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            mvcProductDetail testDetails = new mvcProductDetail()
            {
                Id = 1,
                Name = "Test Details",
                CategoryId = 1,
                Price = 200,
                Quantity = 5,
                ShortDescription = "Entering Test details that should fail.",
                SmallImageId = 1009,
            };

            //Act
            HttpResponseMessage response = WebApiClient.PutAsJsonAsync("ProductDetails" + testDetails.Id.ToString(), testDetails).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            WebApiClient.Dispose();
        }

        [TestMethod]
        public void DeleteProductDetails_ShouldReturnNotFound()
        {
            //Arrange
            HttpClient WebApiClient = new HttpClient();
            WebApiClient.BaseAddress = new Uri("https://localhost:44316/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            HttpResponseMessage response = WebApiClient.DeleteAsync("ProductDetails/1").Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            WebApiClient.Dispose();
        }
    }
}
