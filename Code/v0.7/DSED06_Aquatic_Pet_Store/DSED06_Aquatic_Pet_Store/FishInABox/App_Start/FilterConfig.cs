using System.Web;
using System.Web.Mvc;

namespace FishInABox
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //Enable when going live
            filters.Add(new AuthorizeAttribute());
        }
    }
}
