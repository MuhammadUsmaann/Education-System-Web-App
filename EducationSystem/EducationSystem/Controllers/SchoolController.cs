using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models.Entities;
using EducationSystem.Models;
using EducationSystem.Models.DapperClasses;
using EducationSystem.Filters;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    public class SchoolController : Controller
    {

        SchoolDetails _sd = new SchoolDetails();
        
        public ActionResult Index()
        {
            if (!Auth.Authentication((UserProfileSessionData)Session[Configuration.SESSIONNAME]))
            {
                return RedirectToAction("Index", "Login");
            }

            LocationDetail location = new LocationDetail();


            //ViewBag.Cities = location.GetAll();
            var result = _sd.GetAll().ToList();
            return View(result);
        }

        /****************** POST Methods **********************/
        
        [HttpPost]
        public string Find(int id)
        {
            var s = _sd.FindSingle(id).ToJSON();
            if( s == null)
                return (false).ToJSON();
            return s;
        }
        [HttpPost]
        public string Add(string name, int city_id, string phone)
        {
            School s = new School() { name = name, city_id = city_id, phone = phone };
            bool result = _sd.Insert(s);
            return result.ToJSON();
        }
        
        [HttpPost]
        public string Update(int id, string name, int city_id, string phone)
        {
            School s = new School() { id = id, name = name, city_id = city_id, phone = phone };
            bool result = _sd.Update(s);
            return result.ToJSON();
        }

        [HttpPost]
        public string Delete(int id)
        {
            bool result = _sd.Delete(id);
            return result.ToJSON();
        }
	}
}