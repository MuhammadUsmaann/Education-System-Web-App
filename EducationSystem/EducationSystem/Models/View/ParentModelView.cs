using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.View
{
    public class ParentModelView
    {
        [Required]
        [Display(Name = "Gardian First Name")]
        public string GardianFirstName { get; set; }
        [Required]
        [Display(Name = "Gardian Last Name")]
        public string GardianLastName { get; set; }

        [Display(Name = "Parent First Name")]
        public string ParentFirstName { get; set; }
        
        [Display(Name = "Parent First Name")]
        public string ParentLastName { get; set; }

        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Required]
        [Display(Name = "Profession")]
        public string Profession { get; set; }

        [Required]
        [Display(Name = "Income")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Income { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Landline Number")]
        [DataType(DataType.PhoneNumber)]
        public string Landline { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "ID Card Number (CNIC)")]
        [DataType(DataType.Text)]
        public string CNICNumber { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

    }
}