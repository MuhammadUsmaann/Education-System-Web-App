using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class ChangePassword
    {

        [Required]
        [Display(Name="Current Password")]
        public string currentPassword { get; set; }

        [Required]
        [Display(Name="New Password")]
        [MinLength(6, ErrorMessage = "{0} length must be greater than {1} character.")]
        public string newPassword { get; set; }


        [Required]
        [Display(Name="Confirm Password")]
        [MinLength (6,ErrorMessage="{0} length must be greater than {1} character.")]
        [Compare("newPassword",ErrorMessage="Password does not match.")]
        public string confirmPassword { get; set; }
    }
}