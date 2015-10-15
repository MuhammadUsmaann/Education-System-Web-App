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
    [AuthorizeRole(RoleCode.ADMIN,RoleCode.TEACHER)]
    public class ExamController : Controller
    {
        ExamDetails _ed = new ExamDetails();
        public ActionResult Index()
        {
            int school_id = SessionHandler.GetSchoolID();

            var exam_types = _ed.GetAll(school_id).ToList();
            return View(exam_types);
        }

        [HttpPost]
        public string UpdateStudentMark(int exam_id, int marks, string comment="")
        {
            int school_id =SessionHandler.GetSchoolID();
            int user_id = SessionHandler.GetUserID();
            var result = _ed.UpdateStudentMarks(exam_id,marks,comment );
            return result.ToJSON();
        }
        public string UpdateAllStudentMarks(int exam_id, int subject_id, int class_id,List<SubjectExam> data)
        {

            
            return "";
        }

        public ActionResult StudentMarks(int class_id = 0, int subject_id = 0, int exam_id = 0)
        {
            int school_id = SessionHandler.GetSchoolID();
            int user_id = SessionHandler.GetUserID();
            SubjectDetails sbs = new SubjectDetails();
            ClassDetails clss = new ClassDetails();

            ViewBag.subjects = sbs.GetAll(school_id).ToList().ToJSON();
            ViewBag.classes = clss.GetAll(school_id);
            ViewBag.exams = _ed.GetAll(school_id).ToList();

            if (school_id != 0 && class_id != 0 && subject_id != 0 && exam_id != 0)
            {
                StudentDetails stds = new StudentDetails();

                var stnds_result = stds.GetAll(school_id, class_id).ToList();

                var rests = _ed.FindMarksOnCondition(school_id, class_id, subject_id, exam_id);

                if (rests.ToList().Count == 0 || stnds_result.Count != rests.ToList().Count)
                {

                    foreach (var std in stnds_result)
                    {
                        if (rests.ToList().Count == 0)
                        {
                            _ed.InsertSubjectMarks(school_id, subject_id, exam_id, class_id, std.user_id, user_id);
                        }
                        else
                        {
                            bool isExisted = false;
                            foreach (var se in rests)
                            {
                                if (se.exam_id == exam_id && se.subject_id == subject_id && class_id == se.class_id && se.student_id == std.user_id)
                                    isExisted = true;
                            }

                            if (!isExisted)
                            {
                                _ed.InsertSubjectMarks(school_id, subject_id, exam_id, class_id, std.user_id, user_id);
                            }
                        }
                    }
                }

                ViewBag.class_id = class_id;
                ViewBag.subject_id = subject_id;

                var rest = _ed.FindMarksOnCondition(school_id, class_id, subject_id, exam_id);

                return View(rest);
            }
            return View();
        }
        public ActionResult ExamMarks(int class_id = 0, int subject_id = 0)
        {
            int school_id = SessionHandler.GetSchoolID();
            var exam_types = _ed.GetAll(school_id).ToList();

            if (school_id != 0 && class_id != 0 && subject_id != 0)
            {       
                var rests = _ed.FindOnCondition(school_id,class_id, subject_id);

                if (rests.ToList().Count == 0 || exam_types.Count != rests.ToList().Count)
                {

                    foreach (var exam in exam_types)
                    {
                        if (rests.ToList().Count == 0)
                        {
                            _ed.InsertSubjectExam(school_id, subject_id, exam.exam_id, class_id, 1);
                        }
                        else {
                            foreach (var se in rests)
                            {
                                if (se.exam_id == exam.exam_id && se.subject_id == subject_id && class_id == se.class_id)
                                    continue;
                                _ed.InsertSubjectExam(school_id, subject_id, exam.exam_id, class_id, 1);
                            }
                        }
                    }
                }

                ViewBag.class_id = class_id;
                ViewBag.subject_id = subject_id;
                SubjectDetails sbs = new SubjectDetails();
                ClassDetails clss = new ClassDetails();

                ViewBag.subjects = sbs.GetAll(school_id).ToList().ToJSON();
                ViewBag.classes = clss.GetAll(school_id);

                var rest = _ed.FindOnCondition(school_id, class_id, subject_id);

                return View(rest);
            }
            else
            {
                SubjectDetails sbs = new SubjectDetails();
                ClassDetails clss = new ClassDetails();

                ViewBag.subjects = sbs.GetAll(school_id).ToList().ToJSON();
                ViewBag.classes = clss.GetAll(school_id);

                return View();
            }
        }

        
        /****************** POST Methods **********************/

        [HttpPost]
        public string UpdateExamMarks( int marks, int id , string comment) {
            
            int school_id = SessionHandler.GetSchoolID();
            int user_id = SessionHandler.GetUserID();

            SubjectExam _se = new SubjectExam() { 
                id = id,
                marks = marks,
                comment = comment,
                updated_by = user_id,
                school_id = school_id,
            };

            var result = _ed.UpdateExamSubjectMarks(_se);

            if (!result)
            {
                return result.ToJSON();
            }
            return result.ToJSON();
        }
    

        [HttpPost]
        public string Find(int id)
        {
            int school_id = SessionHandler.GetSchoolID();
            var s = _ed.FindSingle(id, school_id).ToJSON();
            if (s == null)
                return (false).ToJSON();
            return s;
        }
        [HttpPost]
        public string Add(string name, string comment, string date)
        {
            int school_id = SessionHandler.GetSchoolID();

            Exam s = new Exam() { name = name, comment = comment, date = date, school_id = school_id };
            bool result = _ed.Insert(s);
            return result.ToJSON();
        }

        [HttpPost]
        public string Update(int id, string name, string comment, string date)
        {
            int school_id = SessionHandler.GetSchoolID();
            Exam s = new Exam() { exam_id = id, name = name,date=date, comment = comment, school_id = school_id };
            bool result = _ed.Update(s);
            return result.ToJSON();
        }

        [HttpPost]
        public string Delete(int id)
        {
            bool result = _ed.Delete(id);
            return result.ToJSON();
        }
    }
}