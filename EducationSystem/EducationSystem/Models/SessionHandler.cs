using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models
{
    public static class SessionHandler
    {
        public static bool Validate(UserProfileSessionData up)
        {
            Auth ls = new Auth();
            return false;
        }

        public static string GetRoleCode()
        {
            return HttpContext.Current.Session[Configuration.SESSION_ROLE] != null ? (string)HttpContext.Current.Session[Configuration.SESSION_ROLE]:"";
        }
        public static string GetShoolName()
        {
            return HttpContext.Current.Session[Configuration.SESSION_USER_SCHOOL_NAME] != null ? (string)HttpContext.Current.Session[Configuration.SESSION_USER_SCHOOL_NAME] : "Dummy School Name";
        }
        public static int GetUserID()
        {
            return HttpContext.Current.Session[Configuration.SESSION_USER_ID] != null ? (int)HttpContext.Current.Session[Configuration.SESSION_USER_ID] : -1;
        }
        public static int GetSchoolSessionID()
        {
            return HttpContext.Current.Session[Configuration.SESSION_SESSION_ACTIVE] != null 
                && HttpContext.Current.Session[Configuration.SESSION_SESSION_ID] != null
                && (bool)HttpContext.Current.Session[Configuration.SESSION_SESSION_ACTIVE] != false
                ? (int)HttpContext.Current.Session[Configuration.SESSION_SESSION_ID] : -1;
        }
        public static int GetSchoolID()
        {
            return HttpContext.Current.Session[Configuration.SESSION_USER_SCHOOL_ID] != null ?(int)HttpContext.Current.Session[Configuration.SESSION_USER_SCHOOL_ID] : -1;
        }
        public static string GetSchoolCode()
        {
            return HttpContext.Current.Session[Configuration.SESSION_USER_SCHOOL_CODE] != null ? (string)HttpContext.Current.Session[Configuration.SESSION_USER_SCHOOL_CODE] : "NO Code";
        }
        public static string GetUserFullName()
        {
            return HttpContext.Current.Session[Configuration.SESSION_USER_FULLNAME] != null ? (string)HttpContext.Current.Session[Configuration.SESSION_USER_FULLNAME] : null;
        }

    }
    [Serializable]
    public class UserProfileSessionData
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string fullname { get; set; }
        public string school_code { get; set; }
        public string role_code { get; set; }
        public int school_id { get; set; }
        public int session_id { get; set; }
        public bool session_is_active { get; set; }
        public string school_name { get; set; }
    }
}