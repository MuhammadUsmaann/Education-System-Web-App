using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Parent
    {
        public int user_id { get; set; }
        public string gfirst_name { get; set; }
        public string glast_name { get; set; }
        public string gmiddle_name { get; set; }
        public string full_name { get { return gfirst_name + " " + glast_name; } }
        public string gender { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string updated_date { get; set; }
        public int updated_by { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string created_date { get; set; }
        public int created_by { get; set; }
        public int school_id { get; set; }
        public string role_code { get; set; }
        public string profession { get; set; }
        public int income { get; set; }
        public string pfirst_name{ get; set; }
        public string plast_name { get; set; }
        public string mother_name { get; set; }
        public string email { get; set; }
        public string landline { get; set; }
        public string cnic { get; set; }
        public string sys_id { get; set; }
    }
}