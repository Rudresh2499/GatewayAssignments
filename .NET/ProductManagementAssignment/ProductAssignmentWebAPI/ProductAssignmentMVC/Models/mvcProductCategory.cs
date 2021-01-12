using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAssignmentMVC.Models
{
    public class mvcProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcProductCategory()
        {
            this.ProductDetails = new HashSet<mvcProductDetail>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(50,ErrorMessage = "The product category should be less than 50 characters")]
        public string CategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcProductDetail> ProductDetails { get; set; }
    }
}