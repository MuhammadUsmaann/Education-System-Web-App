using EducationSystem.Filters;
using EducationSystem.Models;
using EducationSystem.Models.DapperClasses;
using EducationSystem.Models.Entities;
using EducationSystem.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN)]
    public class StudentController : Controller
    {
        StudentDetails _sd = new StudentDetails();
        //
        // GET: /Student/
        public ActionResult Index()
        {
            int school_id =SessionHandler.GetSchoolID();
            var students = _sd.GetAll(school_id);

            var parents = new ParentDetails().GetAll(school_id);
            var classes = new ClassDetails().GetAll(school_id);

            foreach(Student std in students)
            {
                foreach(Parent p in parents)
                {
                    if(std.parent_id == p.user_id)
                    {
                        std.parent_name = p.full_name;
                    }
                }
                foreach (Class c in classes)
                {
                    if (std.class_id == c.class_id)
                    {
                        std.class_name = c.name;
                    }
                }
            }


            return View(students);
        }
        public ActionResult Add()
        {
            int school_id = SessionHandler.GetSchoolID();
            ParentDetails _pd = new ParentDetails();
            ClassDetails _cd = new ClassDetails();
            ViewBag.parents = _pd.GetAll(school_id);
            ViewBag.classes = _cd.GetAll(school_id);
            return View();
        }
        public ActionResult Edit()
        {
            int school_id = SessionHandler.GetSchoolID();
            ParentDetails _pd = new ParentDetails();
            ClassDetails _cd = new ClassDetails();
            ViewBag.parents = _pd.GetAll(school_id);
            ViewBag.classes = _cd.GetAll(school_id);
            return View();
        }

        [HttpPost]
        public string Add(StudentModelView student)
        {
            if(ModelState.IsValid)
            {
                int session_id = SessionHandler.GetSchoolSessionID();

                if(session_id < 0)
                {
                    return Utils.Response(false, "Please select a default session before adding session");
                }

                int user_id = SessionHandler.GetUserID();

                int school_id = SessionHandler.GetSchoolID();

                var std = new Student()
                {
                    first_name = student.FirstName,
                    last_name = student.LastName,
                    gender = student.Gender,
                    parent_id = student.Parent,
                    class_id = student.Class,
                    school_id= school_id,
                    roll = student.RollNo,
                    monthly_fee = student.MonthlyFee,
                    admission_fee = student.AdmissionFee,
                    examination_fee = student.ExaminationFee,
                    other_charges = student.OtherCharges,
                    discount = student.Discount,
                    email = student.EmailAddress,
                    password = student.Password,
                    address = student.Address,
                    birthday = student.DateofBirth
                };
                std.school_id = school_id;
                std.created_by = user_id;

                int student_id = _sd.Insert(std);

                if (student_id != -1)
                {
                    var session = new SessionDetails().FindSingle(session_id ,school_id);

                    var months = Utils.GetMonths(session.start_date, session.end_date);

                    SchoolFeeStructure feestr = Utils.GetFeeStructure(school_id);
                    insertStudentFeeDetail(FeeType.ADMISSION_FEE, student_id, std.class_id,session_id);
                    insertStudentFeeDetail(FeeType.EXAMINATION_FEE, student_id, std.class_id, session_id);
                    insertStudentFeeDetail(FeeType.OTHER_CHARGES, student_id, std.class_id, session_id);

                    foreach(var month in  months)
                    {
                        insertStudentFeeDetail(FeeType.MONTHLY_FEE, student_id, std.class_id, session_id, month);
                    }

                    return student_id.ToJSON();
                }

                return true.ToJSON();
            }
            return false.ToJSON();
        }

        [HttpGet]
        public ActionResult Edit(int id, StudentModelView student)
        {
            int school_id = SessionHandler.GetUserID();
            var result = _sd.FindSingle(id, school_id);

            ParentDetails _pd = new ParentDetails();
            ClassDetails _cd = new ClassDetails();

            ViewBag.parents = _pd.GetAll(school_id);
            ViewBag.classes = _cd.GetAll(school_id);

            return View(result);
        }

        
        private bool insertStudentFeeDetail(string type , int student_id, int class_id,int session_id,string month="")
        {
            StudentFee stdFee = new StudentFee();
            
            stdFee.paid_fee = 0;
            stdFee.paid_status = FeePaidStatus.UNPAID;
            stdFee.school_id = SessionHandler.GetSchoolID();
            stdFee.class_id = class_id;
            stdFee.updated_by = stdFee.created_by = SessionHandler.GetUserID();
            stdFee.type = type;
            stdFee.student_id = student_id;
            stdFee.fee = 0;
            stdFee.session_id = session_id;
            stdFee.month = month;

            var fees = new StudentFeeDetails();

            fees.Insert(stdFee);
            
            return true;
        }
        [HttpPost]
        public string Edit(StudentModelView student)
        {
            if (ModelState.IsValid)
            {
                int user_id = SessionHandler.GetUserID();

                int school_id = SessionHandler.GetSchoolID();

                var std = new Student()
                {
                    first_name = student.FirstName,
                    last_name = student.LastName,
                    gender = student.Gender,
                    parent_id = student.Parent,
                    class_id = student.Class,
                    school_id = school_id,
                    roll = student.RollNo,
                    monthly_fee = student.MonthlyFee,
                    admission_fee = student.AdmissionFee,
                    examination_fee = student.ExaminationFee,
                    other_charges = student.OtherCharges,
                    discount = student.Discount,
                    email = student.EmailAddress,
                    password = student.Password,
                    address = student.Address,
                    birthday = student.DateofBirth
                };
                std.school_id = school_id;
                std.created_by = user_id;

                int student_id = _sd.Insert(std);

                if (student_id != -1)
                {
                    return student_id.ToJSON();
                }

                return true.ToJSON();
            }

            return false.ToJSON();
        }
    }
}