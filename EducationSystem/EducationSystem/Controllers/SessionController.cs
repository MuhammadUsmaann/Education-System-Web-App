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
    public class SessionController : Controller
    {
        SessionDetails _sd = new SessionDetails(); 
        //
        // GET: /Session/
        public ActionResult Index()
        {
            var result = _sd.GetAll(SessionHandler.GetSchoolID());
            return View(result);
        }
        [HttpPost]
        public string Find(int id)
        {
            return _sd.FindSingle(id, SessionHandler.GetSchoolID()).ToJSON();
        }
        [HttpPost]
        public string Edit(string start, string end,int id )
        {
            if (start.IsValidDate() && end.IsValidDate())
            {
                if (start.IsLowerDate(end))
                {
                    int school_id = SessionHandler.GetSchoolID();
                    int user_id = SessionHandler.GetUserID();

                    Session dates = new Session()
                    {
                        session_id= id,
                        start_date = start,
                        end_date = end,
                        school_id = school_id,
                        updated_by = user_id,
                    };
                    _sd.Update(dates);
                    return true.ToJSON();
                }
            }
            return true.ToJSON();
        }
        [HttpPost]
        [ActionName("default")]
        public string MakeDefault(int id)
        {
            if (id > 0)
            {
                return _sd.MakeDefault(id, SessionHandler.GetSchoolID(), SessionHandler.GetUserID()).ToJSON();
            }
            return false.ToJSON();
        }
        [HttpPost]
        public string Add(string start, string end)
        {
            if (start.IsValidDate() && end.IsValidDate())
            {
                if (start.IsLowerDate(end))
                {
                    int school_id = SessionHandler.GetSchoolID();
                    int user_id = SessionHandler.GetUserID();

                    Session dates = new Session()
                    {
                        start_date = start,
                        end_date = end,
                        school_id = school_id,
                        updated_by = user_id,
                    };
                    _sd.Insert(dates);
                    return true.ToJSON();
                }
                else
                {
                    return false.ToJSON();
                }
            }
            return false.ToJSON();
        }
        public string Delete(int[] ids)
        {
            foreach (int id in ids)
            {
                if (id > 0)
                {
                    _sd.Delete(id);
                }
            }
           
            return true.ToJSON();
        }
	}

}