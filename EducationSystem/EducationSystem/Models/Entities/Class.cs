using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Class
    {
        public int class_id { get; set; }
        public string name { get; set; }
        public int numeric_num { get; set; }
        public int school_id { get; set; }
        public string school_name { get; set; }
        public int teacher_id { get; set; }
        public string teacher_name { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}