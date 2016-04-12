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
        private static AjaxOptions _ajaxOptions;
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

        public static AjaxOptions _AjaxOption()
        {
            return _ajaxOptions ?? new AjaxOptions
            {
                UpdateTargetId = "pMain",
                InsertionMode = InsertionMode.Replace,
                HttpMethod = "POST"
            };
        }

    }
}