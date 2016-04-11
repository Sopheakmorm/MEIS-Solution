﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEIS.Patterns;

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
            //var db = SingletonObject.Context();
            //var user = db.TbUser.FirstOrDefault(x => x.TableKey == 201604110001);
            //var list = db.VPermissionDetails(user);
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