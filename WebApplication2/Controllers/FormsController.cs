using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MEIS.Models;

namespace MEIS.Controllers
{
    public class FormsController : Controller
    {
        private dbMEIS db = new dbMEIS();

        // GET: Forms
        public async Task<ActionResult> Index()
        {
            return View(await db.TbForm.ToListAsync());
        }

        // GET: Forms/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = await db.TbForm.FindAsync(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        // GET: Forms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TableKey,FormName,ModuleId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Notes,StaffId,isDeleted")] Form form)
        {
            if (ModelState.IsValid)
            {
                db.TbForm.Add(form);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(form);
        }

        // GET: Forms/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = await db.TbForm.FindAsync(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TableKey,FormName,ModuleId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Notes,StaffId,isDeleted")] Form form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(form).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        // GET: Forms/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = await db.TbForm.FindAsync(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            Form form = await db.TbForm.FindAsync(id);
            db.TbForm.Remove(form);
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
    }
}
