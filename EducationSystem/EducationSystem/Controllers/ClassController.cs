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
    public class ClassController : Controller
    {
        ClassDetails _cd = new ClassDetails();
        //
        // GET: /Parent/
        public ActionResult Index()
        {
            int school_id = SessionHandler.GetSchoolID();
            TeacherDetail _td = new TeacherDetail();
            var teachers = _td.GetAll(school_id);

            ViewBag.teachers_list = teachers;
            var result = _cd.GetAll(school_id).ToList();
            foreach(var cls in result)
            {
                foreach(var teacher in teachers)
                {
                    if (cls.teacher_id == teacher.user_id)
                    {
                        cls.teacher_name = teacher.full_name;
                    }
                }
            }
            return View(result);
        }
        [HttpPost]
        public string Find(int id)
        {
            int school_id = SessionHandler.GetSchoolID();
            var s = _cd.FindSingle(id, school_id).ToJSON();
            return s;
        }
        [HttpPost]
        public string Add(string name, int numeric, int teacher)
        {
            int user_id =SessionHandler.GetUserID();

            if (user_id == -1)
            {
                return (false).ToJSON();
            }
            int school_id = SessionHandler.GetSchoolID();
            Class s = new Class()
            {
                name = name,
                numeric_num = numeric,
                school_id = school_id,
                teacher_id = teacher,
                created_by = user_id,
                updated_by = user_id,
            };
            bool result = _cd.Insert(s);
            if (!result)
            {
                return result.ToJSON();
            }
            return result.ToJSON();
        }
        [HttpPost]
        public string Update(int id, string name, int numeric, int teacher)
        {
            int user_id = SessionHandler.GetUserID();

            if (user_id == -1)
            {
                return (false).ToJSON();
            }

            int school_id = SessionHandler.GetSchoolID();

            Class s = new Class()
            {
                class_id = id,
                name = name,
                numeric_num = numeric,
                school_id = school_id,
                teacher_id = teacher,
                updated_by = user_id,
            };
            bool result = _cd.Update(s);
            if (!result)
            {
                return result.ToJSON();
            }
            return result.ToJSON();
        }
        
        [HttpPost]
        public string Delete(int[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                foreach (int id in ids)
                {
                    bool result = _cd.Delete(id);
                }
                return true.ToJSON();
            }
            return false.ToJSON();
        }


	}
}