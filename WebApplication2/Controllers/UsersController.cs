using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MEIS.Models;
using MEIS.Patterns;
using WebApplication2.Utils;

namespace MEIS.Controllers
{
    public class UsersController : Controller
    {
        private dbMEIS db = new dbMEIS();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var list = await db.TbUser.ToListAsync();
            return View(list);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.TbUser.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "TableKey,UserName,Password,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Notes,StaffId,isDeleted")] User user)
        {
            if (ModelState.IsValid)
            {
                db.TbUser.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.TbUser.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "TableKey,UserName,Password,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Notes,StaffId,isDeleted")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.TbUser.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            User user = await db.TbUser.FindAsync(id);
            db.TbUser.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Password = Cryptography.Encrypt(model.Password);
            var result = await CheckUserAsyn(model);
            if (result != null)
            {
                result.KeepMeLogIn = model.KeepMeLogIn;
                StoreUser(result);
                db.SaveChanges();
                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            var userlog = (User)Session[SessionIndex.UserLogin.ToString()];
            if (userlog == null)
            {
                var cookie = Request.Cookies["User"];
                if (!string.IsNullOrEmpty(cookie?.Value))
                {
                    var userid = FormsAuthentication.Decrypt(cookie.Value);
                    if (!string.IsNullOrEmpty(userid?.UserData))
                    {
                        var id = decimal.Parse(userid.UserData);
                        userlog = SingletonObject.Context().TbUser.Find(id);
                    }
                }
            }
            if (userlog != null && !userlog.KeepMeLogIn) Response.Cookies.Remove("User");
            Session[SessionIndex.UserLogin.ToString()] = null;
            return RedirectToAction("Login");
        }

        private void StoreUser(User model)
        {
            if (model.KeepMeLogIn)
            {
                // Cookie
                var ticket = new FormsAuthenticationTicket(1,"User",DateTime.Now,DateTime.Now.AddMonths(1),false,model.TableKey.ToString(CultureInfo.InvariantCulture));
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie("User",encryptedTicket);
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
            }
            else
            {
                // Session
                Response.Cookies.Remove("User");
                Session[SessionIndex.UserLogin.ToString()] = model;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            return RedirectToAction("Index", "AdminLte");
        }

        public async Task<User> CheckUserAsyn(User user)
        {
            var obj = db.TbUser.FirstOrDefault(x => x.Password == user.Password && x.UserName == user.UserName);
            return obj;
        }


    }
}