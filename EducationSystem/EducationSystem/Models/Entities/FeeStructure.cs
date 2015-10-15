using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class FeeStructure
    {
        public int fee_id { get; set; }
        public string type { get; set; }
        public int school_id { get; set; }
        public int fee { get; set; }
        public int updated_by { get; set; }
    }

    public class SchoolFeeStructure
    {
        public int class_id { get; set; }
        public int AdmissionFee { get; set; }
        public int ExaminationFee { get; set; }
        public int OtherCharges { get; set; }
    }
}