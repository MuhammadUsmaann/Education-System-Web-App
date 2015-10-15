using EducationSystem.Filters;
using EducationSystem.Models;
using EducationSystem.Models.DapperClasses;
using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN, RoleCode.TEACHER, RoleCode.STUDENT, RoleCode.PARENT)]
    public class AttendanceController : Controller
    {
        AttendanceDetails _ad = new AttendanceDetails();

        public ActionResult ViewClass(int class_id = 0, string date = "")
        {
            ClassDetails _cd = new ClassDetails();
            int school_id = SessionHandler.GetSchoolID();

            ViewBag.classes = _cd.GetAll(school_id);
            ViewBag.Days = 0;

            ViewBag.class_id = class_id;
            ViewBag.date = date;


            if (class_id != 0 && date != "")
            {
                string startDate = "";
                string endDate = "";
                date.makeDateString(ref startDate, ref endDate);

                int daysInMonth = date.GetDaysInMonth();

                var result = _ad.GetAll(school_id, class_id, startDate, endDate).ToList();
                var _sd = new StudentDetails();
                var stds = _sd.GetAll(school_id, class_id);

                ViewBag.Days = daysInMonth;

                List<StudentAttendance> attendance = new List<StudentAttendance>();

                foreach (var std in stds)
                {
                    StudentAttendance stdAttnd = new StudentAttendance() { student_name = std.first_name + " " + std.last_name, student_id = std.user_id };
                    for (int x = 1; x <= daysInMonth; x++)
                    {
                        PresentStatus _ps = new PresentStatus() { dayOfMonth = x, status = -1 };

                        foreach (var atnd in result)
                        {
                            int dayInAtnd = atnd.date.GetDayOfMonth();

                            if (atnd.student_id == std.user_id && x == dayInAtnd)
                            {
                                _ps.status = atnd.status;
                                break;
                            }
                        }
                        stdAttnd.AttendanceDays.Add(_ps);
                    }
                    attendance.Add(stdAttnd);
                }
                return View(attendance);
            }
            return View();
        }
        public ActionResult MarkClass(int class_id = 0, string date = "")
        {
            if (Session[Configuration.SESSION_USER_ID] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ClassDetails _cd = new ClassDetails();
            int school_id = SessionHandler.GetSchoolID();
            ViewBag.class_id = class_id;
            ViewBag.date = date;

            ViewBag.classes = _cd.GetAll(school_id);
            ViewBag.Days = 0;

            if (class_id != 0)
            {
                var result = _ad.GetAll(school_id, class_id).ToList();
                var _sd = new StudentDetails();
                var stds = _sd.GetAll(school_id, class_id);

                var adResult = _ad.GetAll(school_id, class_id, date).ToList();

                //int daysInMonth = this.GetDaysInMonth(date);
                ViewBag.Days = 1;

                var day = date.GetDayOfMonth();
                List<StudentOneDay> attendance = new List<StudentOneDay>();
                foreach (var std in stds)
                {
                    StudentOneDay stdAttnd = new StudentOneDay() { student_name = std.first_name + " " + std.last_name, student_id = std.user_id, status = -1 };
                    attendance.Add(stdAttnd);

                    var isExsited = false;
                    foreach (var adr in adResult)
                    {
                        if (std.user_id == adr.student_id)
                        {
                            isExsited = true;
                        }
                    }
                    if (!isExsited)
                    {
                        Attendance atd = new Attendance() { student_id = std.user_id, school_id = SessionHandler.GetSchoolID(), status = -1, class_id = class_id, date = date, updated_by = SessionHandler.GetUserID() };
                        _ad.Insert(atd);
                    }
                }
                return View(attendance);
            }
            return View();
        }

        [HttpPost]
        public string MarkClass(IEnumerable<StudentOneDay> attendance, string date = "", int class_id = 0)
        {
            if (date == "" || !ModelState.IsValid)
            {
                return false.ToJSON();
            }
            else
            {
                List<Attendance> attnd = new List<Attendance>();
                foreach (var atnd in attendance)
                {
                    atnd.status = atnd.fstatus == false ? 0 : 1;
                    Attendance atd = new Attendance() { student_id = atnd.student_id, school_id = SessionHandler.GetSchoolID(), status = atnd.status, class_id = class_id, date = date, updated_by = SessionHandler.GetUserID() };
                    _ad.Update(atd);
                    // attnd.Add(atd);
                }


            }
            return "";
        }

        public ActionResult ViewStudent(int student_id = 0, int class_id = 0, string date = "")
        {
            var _sdd = new StudentDetails();
            int school_id = SessionHandler.GetSchoolID();

            ViewBag.classes = new ClassDetails().GetAll(school_id);

            ViewBag.class_id = class_id;
            ViewBag.student_id = student_id;
            ViewBag.date = date;

            var stds = _sdd.GetAll(school_id).ToList();

            var sss = (from s in stds
                       select new { fullname = s.first_name + " " + s.last_name, id = s.user_id, cid = s.class_id }).ToList().ToJSON();

            ViewBag.students = sss;

            string startDate = "";
            string endDate = "";
            if (class_id != 0 && student_id != 0 && date != "")
            {
                var std = _sdd.FindSingle(student_id, school_id);
                date.makeDateString(ref startDate, ref endDate);
                int daysInMonth = date.GetDaysInMonth();
                var stdAtnd = _ad.GetAll(school_id, class_id, student_id, startDate, endDate);
                StudentAttendance _sd = GetStudentAttendance(student_id, date);
                return View(_sd);
            }
            return View();
        }

        public ActionResult EditStudent(int student_id = 0, int class_id = 0, string date = "")
        {
            if (Session[Configuration.SESSION_USER_ID] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var _sdd = new StudentDetails();
            int school_id = SessionHandler.GetSchoolID();
            ViewBag.classes = new ClassDetails().GetAll(school_id);
            var stds = _sdd.GetAll(school_id).ToList();

            var sss = (from s in stds
                       select new { fullname = s.first_name + " " + s.last_name, id = s.user_id, cid = s.class_id }).ToList().ToJSON();

            ViewBag.students = sss;

            var std = _ad.FindSingle(student_id, school_id);

            return View(std);
        }
        [HttpPost]
        public string find(int s_id = -1, int class_id = -1, string date = "")
        {
            if (s_id < 0 || date == "" || class_id < 0)
            {
                return false.ToJSON();
            }

            if (Session[Configuration.SESSION_USER_ID] == null)
            {
                return false.ToJSON();
            }
            int school_id = SessionHandler.GetSchoolID();
            var result = _ad.FindSingle(s_id, school_id, date);
            if (result == null)
            {
                Attendance atnd = new Attendance();
                atnd.class_id = class_id;
                atnd.school_id = school_id;
                atnd.student_id = s_id;
                atnd.status = AttendanceStatus.NONE;
                atnd.updated_by = SessionHandler.GetUserID();
                atnd.date = date;
                result = _ad.InsertData(atnd);
            }
            return result.ToJSON();
        }
        [HttpPost]
        public string update(int id = -1, int status = -1)
        {
            if (id < 0 || status < 0)
            {
                return false.ToJSON();
            }
            if (Session[Configuration.SESSION_USER_ID] == null)
            {
                return false.ToJSON();
            }

            int school_id = SessionHandler.GetSchoolID();
            return _ad.Update(id, status, SessionHandler.GetUserID()).ToJSON();
        }

        [ActionName("my")]
        [AuthorizeRole(RoleCode.STUDENT)]
        public ActionResult StudentAttendance(string date = "")
        {
            ViewBag.date = date;

            if (date != "")
            {
                int school_id = SessionHandler.GetSchoolID();
                int user_id = SessionHandler.GetUserID();

                var std = new StudentDetails().FindSingle(user_id, school_id);

                StudentAttendance _sd = GetStudentAttendance(user_id, date);
                return View("StudentAttendance", _sd);
            }
            return View("StudentAttendance");
        }

        [ActionName("kid")]
        [AuthorizeRole(RoleCode.PARENT)]
        public ActionResult KidsAttendance(int kid_id = 0, string date = "")
        {
            ViewBag.date = date;
            var kids = new StudentDetails().GetAllChildren(SessionHandler.GetSchoolID(),SessionHandler.GetUserID());
            ViewBag.kids = kids;
            if (date != "" && kid_id >= 0)
            {
                var kid_obj = (from k in kids
                               where k.user_id == kid_id
                               select k).FirstOrDefault();
                
                ViewBag.kid = kid_obj;

                int school_id = SessionHandler.GetSchoolID();
                StudentAttendance _sd = GetStudentAttendance(kid_id, date);

                return View("KidsAttendance", _sd);
            }
            return View("KidsAttendance");
        }
        private StudentAttendance GetStudentAttendance(int student_id, string date)
        {
            var school_id = SessionHandler.GetSchoolID();
            var std = new StudentDetails().FindSingle(student_id, school_id);

            string startDate = "", endDate = "";

            date.makeDateString(ref startDate, ref endDate);

            int daysInMonth = date.GetDaysInMonth();

            var stdAtnd = _ad.GetAll(school_id, std.class_id, student_id, startDate, endDate);

            StudentAttendance _sd = new StudentAttendance();

            _sd.student_id = std.user_id;
            _sd.student_name = std.first_name + " " + std.last_name;

            for (int i = 0; i < daysInMonth; i++)
            {

                var reqDate = "";
                var dayName = "";

                startDate.getDayAndDate(ref dayName, ref reqDate, i);

                PresentStatus _ps = new PresentStatus();
                _ps.status = -1;
                _ps.date = reqDate;
                _ps.dayName = dayName;

                foreach (var attnd in stdAtnd)
                {
                    if (attnd.date.ComapareDate(reqDate))
                    {
                        _ps.status = attnd.status;
                        break;
                    }
                }
                _sd.AttendanceDays.Add(_ps);
            }
            return _sd;
        }
    }
}