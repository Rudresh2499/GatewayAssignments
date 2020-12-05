using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SourceControlAssignment_1.Models
{
   public class SignUpModel
    {
        [Required]
        [StringLength(50,ErrorMessage = "Name must be less than 50 Characters")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Password must be at least 10 characters long and a combination of uppercase, lowercase letters, digits and special characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [Display(Name = "Re-Type Password")]
        public string CheckPassword { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Phone Number must be atleast 10 digits long without country code")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
        [Required]
        [CreditCard(ErrorMessage = "Invalid Credit Card Number")]
        [Display(Name = "Credit Card Details")]
        public string CreditCardDetails { get; set; }
        [Required]
        [Range(18,65,ErrorMessage = "Age of Applicant must be between 18 years to 65 years")]
        public int Age { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [FileExtensions]
        [Display(Name = "Upload Resume")]
        public string UploadedResume { get; set; }
    }
}