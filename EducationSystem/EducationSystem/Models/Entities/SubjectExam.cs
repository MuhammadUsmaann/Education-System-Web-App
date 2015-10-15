using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class SubjectExam
    {
        public int id { get; set; }
        public int marks { get; set; }
        public string comment { get; set; }

        public int school_id { get; set; }
        public int class_id { get; set; }
        public int subject_id { get; set; }
        public int exam_id { get; set; }
        public int student_id { get; set; }

        public string school_name { get; set; }
        public string class_name { get; set; }
        public string exam_name { get; set; }
        public string subject_name { get; set; }
        public string student_name { get; set; }

        public int created_by { get; set; }
        public int updated_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}