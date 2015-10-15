using EducationSystem.Filters;
using EducationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EducationSystem.Controllers
{
    [SessionExpire]
     [AuthorizeRole(RoleCode.ADMIN, RoleCode.TEACHER, RoleCode.STUDENT, RoleCode.PARENT)]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }
	}
}