using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EducationSystem.Models;
using EducationSystem.Models.Entities;
using System.Web.Security;


namespace EducationSystem.Filters
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        private string _notifyUrl = "/Login";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check  sessions here
            if (HttpContext.Current.Session[Configuration.SESSION_USER_ID] == null)
            {
                filterContext.Result = new RedirectResult(_notifyUrl);
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        private string _notifyUrl = "/Login";
        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        public string NotifyUrl
        {
            get { return _notifyUrl; }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;

            if(httpContext.Session[Configuration.SESSION_USER_ID] == null)
            {
                FormsAuthentication.SignOut();
            }

            var name = HttpContext.Current.User.Identity.Name;
            Role _dbRole = new Auth().GetRole(name);

            if (_dbRole == null)
                return false;

            foreach (var role in allowedroles)
            {
                if (_dbRole.role_code.ToString() == role.ToString())
                {
                    authorize = true; /* return true if Entity has current user(active) with specific role */
                }
            }
            return authorize;
        }
        /// <summary>
        /// this function will check user is authenticated and logged in if not then it will redirect to the log in page other wise it will go normally
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated || filterContext.HttpContext.Session[Configuration.SESSION_USER_ID] == null)
            {
                FormsAuthentication.SignOut();
                filterContext.HttpContext.Response.Redirect(NotifyUrl);
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}