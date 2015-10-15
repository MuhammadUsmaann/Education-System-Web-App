using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models.Entities;
using EducationSystem.Models;
using System.Web.Security;
namespace EducationSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logoff()
        {

            FormsAuthentication.SignOut();
            Session[Configuration.SESSIONNAME] = null;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string Auth(string username, string password)
        {
            Auth auth = new Auth();
            UserProfileSessionData u = auth.Login(username, password);
            if (u != null)
            {
                Session[Configuration.SESSION_ROLE] = u.role_code;
                Session[Configuration.SESSION_USER_ID] = u.user_id;
                Session[Configuration.SESSION_USER_FULLNAME] = u.fullname;
                Session[Configuration.SESSION_USER_USERNAME] = u.username;
                Session[Configuration.SESSION_USER_SCHOOL_ID] = u.school_id;
                Session[Configuration.SESSION_USER_SCHOOL_CODE] = u.school_code;
                Session[Configuration.SESSION_USER_SCHOOL_NAME] = u.school_name != null ? u.school_name : "Dummy Name";
                Session[Configuration.SESSION_SESSION_ACTIVE] = u.session_is_active;
                Session[Configuration.SESSION_SESSION_ID] = u.session_id;
                FormsAuthentication.SetAuthCookie(Session[Configuration.SESSION_USER_USERNAME].ToString(), true);
                return (true).ToJSON();
            }
            return (false).ToJSON();
        }



	}
}