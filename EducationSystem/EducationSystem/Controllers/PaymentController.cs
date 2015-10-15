using EducationSystem.Filters;
using EducationSystem.Models;
using EducationSystem.Models.DapperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN)]
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentFee(int class_id=0,int exam_id = 0)
        {
            if (Session[Configuration.SESSION_USER_ID] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int school_id = SessionHandler.GetSchoolID();
            ExamDetails _ed = new ExamDetails();
            ClassDetails _cd = new ClassDetails();
            ViewBag.exams = _ed.GetAll(school_id).ToList();
            ViewBag.classes = _cd.GetAll(school_id).ToList();

            if (school_id != 0 && class_id != 0 && exam_id != 0)
            {
                StudentDetails _sd = new StudentDetails();

                var stds = _sd.GetAll(school_id,class_id);

                //if (stds.ToList().Count == 0 || exam_types.Count != rests.ToList().Count)
                //{

                //    foreach (var exam in exam_types)
                //    {
                //        if (rests.ToList().Count == 0)
                //        {
                //            _ed.InsertSubjectExam(school_id, subject_id, exam.exam_id, class_id, 1);
                //        }
                //        else
                //        {
                //            foreach (var se in rests)
                //            {
                //                if (se.exam_id == exam.exam_id && se.subject_id == subject_id && class_id == se.class_id)
                //                    continue;
                //                _ed.InsertSubjectExam(school_id, subject_id, exam.exam_id, class_id, 1);
                //            }
                //        }
                //    }
                //}

            }
            return View();
        }
	}
}