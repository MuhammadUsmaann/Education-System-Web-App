using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models.Entities;
using EducationSystem.Models;
using System.IO;
using EducationSystem.Filters;

namespace EducationSystem.Controllers
{
    [SessionExpire]
    [AuthorizeRole(RoleCode.ADMIN, RoleCode.TEACHER, RoleCode.PARENT, RoleCode.STUDENT)]
    public class SettingController : Controller
    {
        //
        // GET: /Setting/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SchoolDate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Viewprofile()
        {
            IEnumerable<Users_detail> _user_details =new Auth().GetUserDetails(HttpContext.User.Identity.Name);
            if (_user_details.FirstOrDefault().image == null) {
                _user_details.FirstOrDefault().image = "~/UserData/dummy.jpg";
            }
            return View(_user_details);
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            Users_detail _user_details = new Auth().GetUserDetails(HttpContext.User.Identity.Name).FirstOrDefault() ;
            return View(_user_details);
        }

        [HttpPost]
        public ActionResult UpdateProfile(Users_detail _details, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    Random _rnd = new Random();

                    string _path =_rnd.Next(500, 100000)+ image.FileName  ;
                    var path = Path.Combine(Server.MapPath("~/UserData/"),
                                            System.IO.Path.GetFileName(_path));
                    image.SaveAs(path);
                    _details.image = "~/UserData/" + _path;

                }
                new Auth().UpdateProfile(_details);
                return RedirectToAction("Viewprofile", "Setting");
            }
            return View(_details);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePassword());
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePassword _passwordModel)
        {
            if (ModelState.IsValid)
            { 
                //db call to verify that it is the authentic user.
                if (!new Auth().IsValidUser(HttpContext.User.Identity.Name , _passwordModel.currentPassword))
                {
                    ModelState.AddModelError("message", "Your Current Password does not match.");
                }
                else {
                    try
                    {
                        new Auth().ChangePassword(HttpContext.User.Identity.Name , _passwordModel.newPassword);
                        ModelState.AddModelError("message", "Your Password has been updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("message", "Sorry! We are facing problem while updating password.");
                    }
                }

            }
            return View(_passwordModel);
        }
	}
}