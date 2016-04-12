using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;
using MEIS.Models;

namespace MEIS.Patterns
{
    public class SingletonObject
    {
        private static dbMEIS context;
        private static AjaxOptions ajaxOptions;
        public static dbMEIS Context()
        {
            return context ?? new dbMEIS();
        }

        public static AjaxOptions AjaxOption(string url)
        {
            return ajaxOptions ?? new AjaxOptions
            {
                Url = url,
                AllowCache = true,
                HttpMethod = "POST",
                InsertionMode = InsertionMode.Replace,
            };
        }
    }
}