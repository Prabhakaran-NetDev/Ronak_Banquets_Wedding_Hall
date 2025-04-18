using System.Web;
using System.Web.Mvc;

namespace Ronak_Banquets_Wedding_Hall
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
