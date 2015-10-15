using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class StudentFee
    {
        public int student_fee_id { get; set; }
        public string paid_status { get; set; }
        public Boolean status{ get; set; }//this property just for form submisstion saving values for month please dont be confused
        public string  type { get; set; }
        public int paid_fee { get; set; }
        public string month { get; set; }
        public string paid_date { get; set; }
        public int fee { get; set; }
        public int student_id { get; set; }
        public int class_id { get; set; }
        public int school_id { get; set; }
        public int session_id { get; set; }
        public string class_name { get; set; }
        public string student_name { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
        public int total_fee { get; set; }
    }
}