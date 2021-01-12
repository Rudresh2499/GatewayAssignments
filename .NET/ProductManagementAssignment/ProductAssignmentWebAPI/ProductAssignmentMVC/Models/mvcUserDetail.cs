using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAssignmentMVC.Models
{
    public partial class mvcUserDetail
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailID { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,20}$", ErrorMessage = "Password must be at least 10 characters long and a combination of uppercase, lowercase letters, digits and special characters")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50,ErrorMessage = "First Name must be less than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "First Name must be less than 50 characters")]
        public string LastName { get; set; }
    }
}