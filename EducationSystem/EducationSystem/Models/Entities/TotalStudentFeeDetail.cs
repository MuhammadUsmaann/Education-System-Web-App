using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class TotalStudentFeeDetail
    {

        public int school_id { get; set; }
        public int class_id { get; set; }
        public int student_id { get; set; }
        public Fee AdmissionFee { get; set; }
        public Fee ExaminationFee { get; set; }
        public Fee Othercharges { get; set; }
        public IEnumerable<Fee> Monthly;
    }
    public class Fee
    {
        public int student_fee_id { get; set; }
        public string type { get; set; }
        public string  paid_status { get; set; }
        public int paid_fee { get; set; }
        public string month { get; set; }
        public int fee { get; set; }
        public int updated_by { get; set; }
    }
}