using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Teacher
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string full_name { get { return first_name + " " + last_name; }}
        public string middle_name { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string updated_date { get; set; }
        public int updated_by { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string created_date  { get; set; }
        public int created_by { get; set; }
        public string other_detail { get; set; }
        public int school_id { get; set; }
        public string role_code { get; set; }
        public string mother_name { get; set; }
        public string email { get; set; }
        public string landline { get; set; }
        public string cnic { get; set; }
        public int salary { get; set; }
        public string father_name { get; set; }
        public string sys_id { get; set; }

    }
}