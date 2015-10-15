using EducationSystem.Filters;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeRoleAttribute());
            filters.Add(new SessionExpireAttribute());
        }
    }
}