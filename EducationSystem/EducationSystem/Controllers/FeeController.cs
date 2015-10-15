using EducationSystem.Filters;
using EducationSystem.Models;
using EducationSystem.Models.DapperClasses;
using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN, RoleCode.STUDENT, RoleCode.PARENT)]
    public class FeeController : Controller
    {
        FeeStructureDetail _fsd = new FeeStructureDetail();
        public ActionResult Index()
        {
            int school_id = SessionHandler.GetSchoolID();
            var fees = _fsd.GetAll(school_id);

            if(fees == null || fees.Count() == 0)
            {
                updateFee(FeeType.OTHER_CHARGES);
                updateFee(FeeType.EXAMINATION_FEE);
                updateFee(FeeType.ADMISSION_FEE); 
            }
            SchoolFeeStructure feeStr = Utils.GetFeeStructure(school_id);
            return View("FeeStructure", feeStr);
        }
        [ActionName("student-fee")]
        public ActionResult StudentFee(int student_id=0, int class_id=0) {

            int school_id = SessionHandler.GetSchoolID();
            var session_id = SessionHandler.GetSchoolSessionID();

            var classes = Utils.GetClasses(school_id);
            var _sd = Utils.GetClassStudent(school_id, class_id);

            var sds = (from s in _sd
                       select new { fullname = s.first_name + " " + s.last_name, id = s.user_id,class_id= s.class_id }).ToList().ToJSON();

            ViewBag.classes = classes;
            ViewBag.student_id = student_id;
            ViewBag.class_id = class_id;
            ViewBag.students = sds;

            if(student_id != 0 && class_id != 0)
            {
                ViewBag.isSelected = true;

                var fees = new StudentFeeDetails().GetAll(school_id, student_id, class_id);
                var std_itself = new StudentDetails().FindSingle(student_id, school_id);
                var parent = new ParentDetails().FindSingle(std_itself.parent_id, school_id);
                var cls = new ClassDetails().FindSingle(std_itself.class_id, school_id);
                std_itself.parent_name = parent.full_name;
                std_itself.class_name = cls.name;

                ViewBag.student = std_itself;

                var add_fee = (from f in fees
                               where f.type == FeeType.EXAMINATION_FEE || f.type == FeeType.ADMISSION_FEE || f.type == FeeType.OTHER_CHARGES
                               select f).ToList();

                var monthly_fee = (from f in fees
                               where f.type == FeeType.MONTHLY_FEE
                               select f).ToList();
                
                foreach (var f in add_fee)
                {
                    if (f.type == FeeType.ADMISSION_FEE)
                        f.fee = std_itself.admission_fee;
                    if (f.type == FeeType.EXAMINATION_FEE)
                        f.fee = std_itself.examination_fee;
                    if (f.type == FeeType.OTHER_CHARGES)
                        f.fee = std_itself.other_charges;
                }

                var total = 0;
                foreach (var f in fees)
                {
                    f.fee = std_itself.monthly_fee;
                    if (f.paid_status == FeePaidStatus.PAID)
                    {
                        total += f.fee;
                    }
                }
                
                //to find the discount amount

                int discount = (int)(from std in _sd
                             where std.user_id == student_id
                               select std.discount).FirstOrDefault();

                ViewBag.total = total - discount;
                ViewBag.discount = discount;
                ViewBag.add_fee = add_fee;
                return View("StudentFee", monthly_fee);
            }
            return View("StudentFee");
        }

        [ActionName("single-report")]
        public ActionResult SingleStudentFeeReport(int student_id = 0, int class_id = 0)
        {
            int school_id = SessionHandler.GetSchoolID();

            var classes = Utils.GetClasses(school_id);
            var _sd = Utils.GetClassStudent(school_id, class_id);

            var sds = (from s in _sd
                       select new { fullname = s.first_name + " " + s.last_name, id = s.user_id, class_id = s.class_id }).ToList().ToJSON();

            
            ViewBag.classes = classes;
            ViewBag.student_id = student_id;
            ViewBag.class_id = class_id;
            ViewBag.students = sds;

            if (student_id != 0 && class_id != 0)
            {
                ViewBag.isSelected = true;
                var fees = new StudentFeeDetails().GetAll(school_id, student_id, class_id);
                var std_itself = new StudentDetails().FindSingle(student_id,school_id);
                var parent = new ParentDetails().FindSingle(std_itself.parent_id, school_id);
                var cls = new ClassDetails().FindSingle(std_itself.class_id, school_id);
                std_itself.parent_name = parent.full_name;
                std_itself.class_name = cls.name;


                ViewBag.student = std_itself;

                var add_fee = (from f in fees
                               where f.type == FeeType.EXAMINATION_FEE || f.type == FeeType.ADMISSION_FEE || f.type == FeeType.OTHER_CHARGES
                               select f).ToList();


                var total = 0;
                
                foreach(var f in add_fee)
                {
                    if (f.type == FeeType.ADMISSION_FEE)
                        f.fee = std_itself.admission_fee;
                    if (f.type == FeeType.EXAMINATION_FEE)
                        f.fee = std_itself.examination_fee;
                    if (f.type == FeeType.OTHER_CHARGES)
                        f.fee = std_itself.other_charges;

                    if(f.paid_status == FeePaidStatus.PAID)
                        total += f.fee;
                }

                var monthly_fee = (from f in fees
                                   where f.type == FeeType.MONTHLY_FEE
                                   select f).ToList();

                foreach (var f in monthly_fee)
                {
                    f.fee = std_itself.monthly_fee;
                    if (f.paid_status == FeePaidStatus.PAID)
                    {
                        total += f.fee;
                    }
                }

                //to find the discount amount

                int discount = std_itself.discount;

                ViewBag.total = total - discount;
                ViewBag.discount = discount;
                ViewBag.add_fee = add_fee;
                return View("SingleStudentFeeReport", monthly_fee);
            }
            return View("SingleStudentFeeReport");
        }

        [ActionName("family-report")]
        public ActionResult FamilyReport(int p_id=0){


            int school_id = SessionHandler.GetSchoolID();

            var parents = new ParentDetails().GetAll(school_id);

            ViewBag.parents = parents;

            if (p_id > 0)
            {
                ViewBag.isSelected = true;

                var stds = new StudentDetails().GetAllChildren(school_id, p_id);
                var length = parents.Count();
                
                if(length > 0)
                {
                    string stds_ids = "(";
                    int i = 0;
                    foreach (var std in stds)
                    {
                        stds_ids += std.user_id;
                        if ((i + 1) != length)
                        {
                            stds_ids += ",";
                        }
                        i++;
                    }
                    stds_ids += ")";

                    ViewBag.children = stds;
                    var student_fees = new StudentFeeDetails().GetAll(school_id, stds_ids);
                    return View("FamilyReport", student_fees);
                }
            }
            return View("FamilyReport");
        }
        [HttpPost]
        public string StudentFee(IEnumerable<StudentFee> stdFee, IEnumerable<StudentFee> stdAdd, int total, int discount,int c_id = -1,int s_id = -1)
        {
            if (stdFee != null || stdFee.Count() > 0 || stdAdd != null || stdAdd.Count() > 0)
            {
                var school_id = SessionHandler.GetSchoolID();
                var user_id = SessionHandler.GetUserID();

                foreach(var fee in stdFee)
                {
                    fee.updated_by = user_id;
                    fee.school_id = school_id;
                    
                    if(fee.status)
                    {
                        fee.paid_status = FeePaidStatus.PAID;
                    }
                    else
                    {
                        fee.paid_status = FeePaidStatus.UNPAID;
                    }
                   _fsd.UpdateStudentFee(fee);
                }
                foreach(var fee in stdAdd)
                {
                    fee.updated_by = user_id;
                    fee.school_id = school_id;
                    
                    if(fee.status)
                    {
                        fee.paid_date = DateTime.Now.ToString("yyyy-mm-dd");
                        fee.paid_status = FeePaidStatus.PAID;
                    }
                    else
                    {
                        fee.paid_status = FeePaidStatus.UNPAID;
                    }

                   _fsd.UpdateStudentFee(fee);
                }

                StudentDetails _sd = new StudentDetails();

                _sd.Update(school_id , s_id,c_id, discount);

            }

            return false.ToJSON();
        }
        [HttpPost]
        public string delete(int s_id = -1, int c_id = -1)
        {
            if(s_id < 0 || c_id < 0)
            {
                return false.ToJSON();
            }

            int school_id = SessionHandler.GetSchoolID();
            int user_id = SessionHandler.GetUserID();

            StudentFee sf = new StudentFee();

            sf.updated_by = user_id;
            sf.paid_status = FeePaidStatus.UNPAID;
            sf.student_id = s_id;
            sf.class_id = c_id;
            sf.school_id = school_id;
            _fsd.Delete(sf);            

            return false.ToJSON();
        }
        [HttpPost]
        public string update(SchoolFeeStructure sfs)
        {
            updateFee(FeeType.ADMISSION_FEE, "update",sfs.AdmissionFee);
            updateFee(FeeType.EXAMINATION_FEE,"update" , sfs.ExaminationFee);
            updateFee(FeeType.OTHER_CHARGES, "update", sfs.OtherCharges);

            return true.ToJSON();
        }
        [HttpPost]
        public string find()
        {
            SchoolFeeStructure feeStr = Utils.GetFeeStructure(SessionHandler.GetSchoolID());
            return feeStr.ToJSON();
        }
        private bool updateFee(string type, string action = "insert", int fee = 0,string month="")
        {
            FeeStructure fs_admin = new FeeStructure();

            int school_id = SessionHandler.GetSchoolID();
            int user_id = SessionHandler.GetUserID();

            fs_admin.type = type;
            fs_admin.school_id = school_id;
            fs_admin.updated_by = user_id;

            if (action == "update")
            {
                fs_admin.fee = fee;
                _fsd.Update(fs_admin);
            }
            else
            {
                _fsd.Insert(fs_admin);
            }
            return true;
        }

        [ActionName("my")]
        public ActionResult MineFee()
        {
            int school_id = SessionHandler.GetSchoolID();
            int student_id = SessionHandler.GetUserID(); 
            ViewBag.isSelected = true;
        
            var std_itself = new StudentDetails().FindSingle(student_id, school_id);
            var fees = new StudentFeeDetails().GetAll(school_id, student_id, std_itself.class_id);

            var parent = new ParentDetails().FindSingle(std_itself.parent_id, school_id);
            var cls = new ClassDetails().FindSingle(std_itself.class_id, school_id);
            std_itself.parent_name = parent.full_name;
            std_itself.class_name = cls.name;
            ViewBag.student = std_itself;

            var add_fee = (from f in fees
                           where f.type == FeeType.EXAMINATION_FEE || f.type == FeeType.ADMISSION_FEE || f.type == FeeType.OTHER_CHARGES
                           select f).ToList();


            var total = 0;

            foreach (var f in add_fee)
            {
                if (f.type == FeeType.ADMISSION_FEE)
                    f.fee = std_itself.admission_fee;
                if (f.type == FeeType.EXAMINATION_FEE)
                    f.fee = std_itself.examination_fee;
                if (f.type == FeeType.OTHER_CHARGES)
                    f.fee = std_itself.other_charges;

                if (f.paid_status == FeePaidStatus.PAID)
                    total += f.fee;
            }

            var monthly_fee = (from f in fees
                               where f.type == FeeType.MONTHLY_FEE
                               select f).ToList();

            foreach (var f in monthly_fee)
            {
                f.fee = std_itself.monthly_fee;
                if (f.paid_status == FeePaidStatus.PAID)
                {
                    total += f.fee;
                }
            }

            //to find the discount amount

            int discount = std_itself.discount;

            ViewBag.total = total - discount;
            ViewBag.discount = discount;
            ViewBag.add_fee = add_fee;
            return View("MineFee", monthly_fee);
        }
        [ActionName("kid")]
        public ActionResult KidFee(int kid_id=0)
        {
            int school_id = SessionHandler.GetSchoolID();

            ViewBag.kids = new StudentDetails().GetAllChildren(school_id, SessionHandler.GetUserID());
            ViewBag.kid_id = kid_id;

            if(kid_id > 0)
            {
                
                int student_id = kid_id;
                ViewBag.isSelected = true;

                var std_itself = new StudentDetails().FindSingle(student_id, school_id);
                var fees = new StudentFeeDetails().GetAll(school_id, student_id, std_itself.class_id);

                var parent = new ParentDetails().FindSingle(std_itself.parent_id, school_id);
                var cls = new ClassDetails().FindSingle(std_itself.class_id, school_id);
                std_itself.parent_name = parent.full_name;
                std_itself.class_name = cls.name;
                ViewBag.student = std_itself;

                var add_fee = (from f in fees
                               where f.type == FeeType.EXAMINATION_FEE || f.type == FeeType.ADMISSION_FEE || f.type == FeeType.OTHER_CHARGES
                               select f).ToList();


                var total = 0;

                foreach (var f in add_fee)
                {
                    if (f.type == FeeType.ADMISSION_FEE)
                        f.fee = std_itself.admission_fee;
                    if (f.type == FeeType.EXAMINATION_FEE)
                        f.fee = std_itself.examination_fee;
                    if (f.type == FeeType.OTHER_CHARGES)
                        f.fee = std_itself.other_charges;

                    if (f.paid_status == FeePaidStatus.PAID)
                        total += f.fee;
                }

                var monthly_fee = (from f in fees
                                   where f.type == FeeType.MONTHLY_FEE
                                   select f).ToList();

                foreach (var f in monthly_fee)
                {
                    f.fee = std_itself.monthly_fee;
                    if (f.paid_status == FeePaidStatus.PAID)
                    {
                        total += f.fee;
                    }
                }
                //to find the discount amount
                int discount = std_itself.discount;

                ViewBag.total = total - discount;
                ViewBag.discount = discount;
                ViewBag.add_fee = add_fee;
                return View("KidFee", monthly_fee);
            }
            return View("KidFee");
        }

	}
}