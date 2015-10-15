using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Exam
    {
        public int exam_id { get; set; }
        public string name { get; set; }
        public string comment{ get; set; }
        public int school_id { get; set; }
        public string date { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}