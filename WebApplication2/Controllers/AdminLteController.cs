using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MEIS.Patterns;
using MEIS.Models;

namespace AdminLteMvc.Controllers
{
    /// <summary>
    /// This is an example controller using the AdminLTE NuGet package's CSHTML templates, CSS, and JavaScript
    /// You can delete these, or use them as handy references when building your own applications
    /// </summary>
    public class AdminLteController : Controller
    {
        /// <summary>
        /// The home page of the AdminLTE demo dashboard, recreated in this new system
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var loginObject = (User)Session[SessionIndex.UserLogin.ToString()];
            if (loginObject == null)
            {
                var cookie = Request.Cookies["User"];
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    var userid = FormsAuthentication.Decrypt(cookie.Value);
                    if (!string.IsNullOrEmpty(userid.UserData))
                    {
                        var id = decimal.Parse(userid.UserData);
                        loginObject = SingletonObject.Context().TbUser.Find(id);
                    }
                }
            }
            if (loginObject == null)
            {
                Session[SessionIndex.UserLogin.ToString()] = loginObject;
                return RedirectToAction("Login", "Users");
            }
            return View();
        }

        /// <summary>
        /// The color page of the AdminLTE demo, demonstrating the AdminLte.Color enum and supporting methods
        /// </summary>
        /// <returns></returns>
        public ActionResult Colors()
        {
            return View();
        }
    }
}