using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class FamilyFeeReport
    {
        public int  parent_id { get; set; }
        public string  parent_name { get; set; }
        public int total_monthly_fee { get; set; }
        public int  noOfMonth { get; set; }
        public int tolal_examinationFee { get; set; }
        public int total_admissionFee { get; set; }
        public int total_otherCharges { get; set; }
        public int total_discount { get; set; }

        public List<Student> childs = new List<Student>();

    }
}