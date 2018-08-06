using System.Web;
using System.Web.Mvc;

namespace Yazlab3_AramaMotoru2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
