using EducationSystem.Models;
using EducationSystem.Models.DapperClasses;
using EducationSystem.Models.Entities;
using EducationSystem.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models.View;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN)]
    public class ParentController : Controller
    {
        ParentDetails _pd = new ParentDetails();

        //
        // GET: /Parent/
        public ActionResult Index()
        {
            int school_id = SessionHandler.GetSchoolID();
            var result = _pd.GetAll(school_id).ToList();
            return View(result);
        }
        /****************** POST Methods **********************/
        [HttpPost]
        public string Find(int id)
        {
            int school_id = SessionHandler.GetSchoolID();
            var s = _pd.FindSingle(id, school_id).ToJSON();
            return s;
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var parent = _pd.FindSingle(id,SessionHandler.GetSchoolID());

            ViewBag.actionURL = "edit";
            ViewBag.id = id;

            ParentModelView pmv = new ParentModelView(){
                GardianFirstName = parent.gfirst_name,
                GardianLastName=parent.glast_name,
                Address = parent.address,
                Phone = parent.phone,
                Password = parent.password,
                Landline = parent.landline,
                CNICNumber= parent.cnic,
                EmailAddress= parent.email,
                Profession = parent.profession,
                Income = parent.income,
                ParentFirstName = parent.pfirst_name,
                ParentLastName = parent.plast_name,
                MotherName= parent.mother_name ,
            };
            return View("Add", pmv);
        }
        [HttpPost]
        public string Add(ParentModelView parent)
        {
            if(ModelState.IsValid)
            {
                int user_id = SessionHandler.GetUserID();

                if (user_id == -1)
                {
                    return (false).ToJSON();
                }
                int school_id = SessionHandler.GetSchoolID();
                Parent s = new Parent()
                {
                    gfirst_name = parent.GardianFirstName,
                    glast_name = parent.GardianLastName,
                    address = parent.Address,
                    phone = parent.Phone,
                    password = parent.Password,
                    school_id = school_id,
                    created_by = user_id,
                    updated_by = user_id,
                    profession = parent.Profession,
                    income = parent.Income,
                    pfirst_name = parent.ParentFirstName,
                    plast_name = parent.ParentLastName,
                    mother_name = parent.MotherName,
                    landline = parent.Landline,
                    cnic = parent.CNICNumber,
                    email = parent.EmailAddress
                };
                bool result = _pd.Insert(s);
                return result.ToJSON();
            }
            return "";
        }
        [HttpPost]
        public string Edit(int id, ParentModelView parent)
        {
            if(ModelState.IsValid)
            {
                int user_id = SessionHandler.GetUserID();

                int school_id = SessionHandler.GetSchoolID();
                Parent s = new Parent()
                {
                    user_id = id,
                    gfirst_name = parent.GardianFirstName,
                    glast_name = parent.GardianLastName,
                    address = parent.Address,
                    phone = parent.Phone,
                    password = parent.Password,
                    school_id = school_id,
                    created_by = user_id,
                    updated_by = user_id,
                    profession = parent.Profession,
                    income = parent.Income,
                    pfirst_name = parent.ParentFirstName,
                    plast_name = parent.ParentLastName,
                    mother_name = parent.MotherName,
                    landline = parent.Landline,
                    cnic = parent.CNICNumber,
                    email = parent.EmailAddress
                };
                bool result = _pd.Update(s);
                return result.ToJSON();
            }
            return false.ToJSON();
        }
        [HttpPost]
        public string Delete(int[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (int id in ids)
                {
                    _pd.Delete(id);
                }
                return true.ToJSON();
            }
            return false.ToJSON();
        }
	}
}