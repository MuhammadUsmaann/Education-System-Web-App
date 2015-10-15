using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Users_detail
    {
        public int id { get; set; }
        [Display(Name="First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Display(Name = "Middle Name")]
        public string middle_name { get; set; }
        public string gender_name { get; set; }
        public string datetime { get; set; }
        public string role_code{ get; set; }
        public string sys_id { get; set; }
        [Display(Name = "Other Details")]
        public string other_detail { get; set; }
        public int school_id { get; set; }
        public int created_by { get; set; }
        public int user_id { get; set; }
        public string created_date { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Profession")]
        public string profession { get; set; }
        public string image { get; set; }
    }
}