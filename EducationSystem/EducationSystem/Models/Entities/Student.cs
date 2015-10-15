using System;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Student
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string full_name { get { return first_name + " " + last_name; } }
        public string roll { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }

        public string gender { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int parent_id { get; set; }
        public string role_code { get; set; }
        public string parent_name { get; set; }
        public int class_id { get; set; }
        public int school_id { get; set; }
        public string school_name { get; set; }
        public string class_name { get; set; }
        public int monthly_fee { get; set; }
        public int admission_fee { get; set; }
        public int examination_fee { get; set; }
        public int other_charges { get; set; }
        public int discount { get; set; }
        public string  sys_id { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
    }
}