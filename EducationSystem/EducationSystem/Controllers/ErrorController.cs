using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("404")]
        public ActionResult Error404(){
            return View();
        }
        [ActionName("500")]
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult NotAutherize()
        {
            return View();
        }
	}
}