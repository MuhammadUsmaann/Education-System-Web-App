using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.View
{
    public class TeacherModelView
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Required]
        [Display(Name = "Salary")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Salary { get; set; }

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