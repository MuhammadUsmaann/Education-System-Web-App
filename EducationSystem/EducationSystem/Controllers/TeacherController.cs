using EducationSystem.Models.DapperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;
using EducationSystem.Models.Entities;
using EducationSystem.Filters;
using EducationSystem.Models.View;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN)]
    public class TeacherController : Controller
    {

        TeacherDetail _td = new TeacherDetail();

        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            int school_id = SessionHandler.GetSchoolID();
            var result = _td.GetAll(school_id).ToList();
            return View(result);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.actionURL = "edit";

            var teacher = _td.FindSingle(id, SessionHandler.GetSchoolID());

           ViewBag.id = id;

            TeacherModelView pmv = new TeacherModelView()
            {
                FirstName = teacher.first_name,
                LastName = teacher.last_name,
                Address = teacher.address,
                Phone = teacher.phone,
                Password = teacher.password,
                Landline = teacher.landline,
                CNICNumber = teacher.cnic,
                EmailAddress = teacher.email,
                Salary= teacher.salary,
                FatherName= teacher.father_name,
                MotherName = teacher.mother_name,
            };
            return View("Add", pmv);
        }
        /****************** POST Methods **********************/
        [HttpPost]
        public string Add(TeacherModelView teacher)
        {
            if (ModelState.IsValid)
            {
                int user_id = SessionHandler.GetUserID();

                int school_id = SessionHandler.GetSchoolID();
                Teacher s = new Teacher()
                {
                    first_name = teacher.FirstName,
                    last_name = teacher.LastName,
                    address = teacher.Address,
                    phone = teacher.Phone,
                    password = teacher.Password,
                    school_id = school_id,
                    created_by = user_id,
                    updated_by = user_id,
                    salary = teacher.Salary,
                    father_name = teacher.FatherName,
                    mother_name = teacher.MotherName,
                    landline = teacher.Landline,
                    cnic = teacher.CNICNumber,
                    email = teacher.EmailAddress
                };
                bool result = _td.Insert(s);
                return result.ToJSON();
            }
            return false.ToJSON();
        }
        [HttpPost]
        public string Find(int id)
        {
            int school_id = SessionHandler.GetSchoolID();
            
            var s = _td.FindSingle(id,school_id).ToJSON();
            return s;
        }
        [HttpPost]
        public string Edit(int id,TeacherModelView teacher)
        {
            if (ModelState.IsValid)
            {
                int user_id = SessionHandler.GetUserID();

                int school_id = SessionHandler.GetSchoolID();
                Teacher s = new Teacher()
                {
                    first_name = teacher.FirstName,
                    last_name = teacher.LastName,
                    address = teacher.Address,
                    phone = teacher.Phone,
                    password = teacher.Password,
                    school_id = school_id,
                    created_by = user_id,
                    updated_by = user_id,
                    salary = teacher.Salary,
                    father_name = teacher.FatherName,
                    mother_name = teacher.MotherName,
                    landline = teacher.Landline,
                    cnic = teacher.CNICNumber,
                    email = teacher.EmailAddress
                };
                bool result = _td.Update(s);
                return result.ToJSON();
            }
            return false.ToJSON();
        }
        [HttpPost]
        public string Delete(int[] ids)
        {
            if(ids.Length>0)
            {
                foreach(int id in ids)
                {
                    _td.Delete(id);
                }
                return true.ToJSON();
            }
            return false.ToJSON();
        }
	}
}