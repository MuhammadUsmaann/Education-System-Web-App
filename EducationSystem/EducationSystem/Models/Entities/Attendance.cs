using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Attendance
    {
        public int attendance_id { get; set; }
        public int status { get; set; }
        public string  date { get; set; }
        public int student_id { get; set; }
        public int school_id { get; set; }
        public int class_id { get; set; }
        public int student_name { get; set; }
        public int class_name { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}