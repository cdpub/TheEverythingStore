using System.Web;
using System.Web.Mvc;

namespace TheEverythingStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // force all requests to use ssl
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
