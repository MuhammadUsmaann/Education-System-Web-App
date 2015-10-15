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
    [AuthorizeRole(RoleCode.ADMIN)]
    public class SubjectController : Controller
    {
        SubjectDetails _sd = new SubjectDetails();
        
        //
        // GET: /Parent/
        public ActionResult Index()
        {
            if (Session[Configuration.SESSION_USER_ID] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int school_id = SchoolID();
            
            TeacherDetail _td = new TeacherDetail();
            ViewBag.teachers_list = _td.GetAll(school_id);
            ClassDetails _cd = new ClassDetails();
            ViewBag.classes_list = _cd.GetAll(school_id);

            var result = _sd.GetAll(school_id).ToList();
            return View(result);
        }
        private int SchoolID()
        {
            if (Session[Configuration.SESSION_USER_ID] != null)
            {
                return Convert.ToInt32(Session[Configuration.SESSION_USER_SCHOOL_ID]);
            }
            return -1;
        }
        /****************** POST Methods **********************/
        [HttpPost]
        public string Find(int id)
        {
            int schoo_id = SchoolID();
            var s = _sd.FindSingle(id, schoo_id).ToJSON();
            return s;
        }
        private int UserID()
        {
            if (Session[Configuration.SESSION_USER_ID] != null)
            {
                int id = Convert.ToInt32(Session[Configuration.SESSION_USER_ID]);
                return id;
            }
            return -1;
        }
        [HttpPost]
        public string Add(string name, int class_id, int teacher)
        {
            int user_id = UserID();

            if (user_id == -1)
            {
                return (false).ToJSON();
            }
            int school = SchoolID();
            Subject s = new Subject()
            {
                name = name,
                class_id = class_id,
                school_id = school,
                teacher_id = teacher,
                created_by = user_id,
                updated_by = user_id,
            };
            bool result = _sd.Insert(s);
            if (!result)
            {
                return result.ToJSON();
            }
            return result.ToJSON();
        }
        [HttpPost]
        public string Update(int id, string name, int class_id, int teacher)
        {
            int user_id = UserID();

            if (user_id == -1)
            {
                return (false).ToJSON();
            }
            int school = SchoolID();
            Subject s = new Subject()
            {
                subject_id = id,
                name = name,
                class_id = class_id,
                school_id = school,
                teacher_id = teacher,
                updated_by = user_id,
            };
            bool result = _sd.Update(s);
            if (!result)
            {
                return result.ToJSON();
            }
            return result.ToJSON();
        }
        [HttpPost]
        public string Delete(int id)
        {
            bool result = _sd.Delete(id);
            return true.ToJSON();
        }


    }
}