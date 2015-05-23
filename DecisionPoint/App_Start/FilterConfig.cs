using System.Web.Mvc;

namespace DecisionPoint
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           filters.Add(new HandleErrorAttribute());
           filters.Add(new DecisionPoint.Models.CheckSessionOutAttribute());
          // filters.Add(new DecisionPoint.Models.NoDirectAccessAttribute());
        }
    }

}