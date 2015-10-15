using System.ComponentModel.DataAnnotations;

namespace EducationSystem.Models.View
{
    public class StudentModelView
    {
        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Parents")]
        public int Parent { get; set; }
        [Display(Name = "Class")]
        public int Class { get; set; }

        [Required]
        [Display(Name = "Roll No#")]
        [DataType(DataType.Text)]
        public string RollNo { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public string DateofBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }


        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Monthly Fee")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int MonthlyFee { get; set; }

        [Required]
        [Display(Name = "Admission Fee")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int AdmissionFee { get; set; }

        [Required]
        [Display(Name = "Examination Fee")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ExaminationFee { get; set; }

        [Required]
        [Display(Name = "Otehr Charges")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int OtherCharges { get; set; }
        [Required]
        [Display(Name = "Discount")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Discount { get; set; }
    }
}