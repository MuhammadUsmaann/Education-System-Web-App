using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class StudentAttendance
    {

        public int student_id { get; set; }
        public string student_name { get; set; }

        public List<PresentStatus> AttendanceDays = new List<PresentStatus>();
    }
    public class PresentStatus
    {
        public int dayOfMonth { get; set; }
        public string date { get; set; }

        public string dayName { get; set; }
        /*
         * 
         * status Value detail and Purpose
         * status  = 1 for student present
         * status  = 0 for student absent
         * status  = -1 for student not Exist
         * 
         */
        
        public int status { get; set; }

    }
    public class StudentOneDay {
        public int student_id { get; set; }
        public string student_name { get; set; }

        public bool fstatus { get; set; }
        public int status { get; set; }
    }

}