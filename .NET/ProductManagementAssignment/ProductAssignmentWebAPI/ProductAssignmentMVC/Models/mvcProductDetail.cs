using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAssignmentMVC.Models
{
    public partial class mvcProductDetail
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(50,ErrorMessage = "Product Name must be less than 50 characters")]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Unit Price of Product")]
        [Range(0,200000.00,ErrorMessage = "Price entered is either negative or above 200k")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Quantity ordered")]
        [Range(1,100000,ErrorMessage = "Quantity entered is zero/negative or greater than 100000")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Brief Description of the product")]
        public string ShortDescription { get; set; }

        [Display(Name = "Detailed description of the product")]
        public string LongDescription { get; set; }

        [JsonIgnore]
        [Required]
        public HttpPostedFileBase SmallImageFile { get; set; }

        public int SmallImageId { get; set; }

        [JsonIgnore]
        public HttpPostedFileBase LargeImageFile { get; set; }

        public Nullable<int> LargeImageId { get; set; }

        public virtual mvcProductCategory ProductCategory { get; set; }
        public virtual mvcSmallImageDetail SmallImageDetail { get; set; }

        public virtual mvcLargeImageDetail LargeImageDetail { get; set; }
    }
}