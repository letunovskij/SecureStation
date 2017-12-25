using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DivisionWebGlobal.DAL;
using DivisionWebGlobal.Models.Data;

namespace DivisionWebGlobal.Controllers
{
    public class HeadTablesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: HeadTables
        public ActionResult Index()
        {
            var dvHeadTables = db.DvHeadTables.Include(h => h.DvHead);
            return View(dvHeadTables.ToList());
        }

        // GET: HeadTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadTable headTable = db.DvHeadTables.Find(id);
            if (headTable == null)
            {
                return HttpNotFound();
            }
            return View(headTable);
        }

        // GET: HeadTables/Create
        public ActionResult Create()
        {
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress");
            return View();
        }

        // POST: HeadTables/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Idhead,Time,ConfigurationTable,ConnectedTable")] HeadTable headTable)
        {
            if (ModelState.IsValid)
            {
                db.DvHeadTables.Add(headTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", headTable.Idhead);
            return View(headTable);
        }

        // GET: HeadTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadTable headTable = db.DvHeadTables.Find(id);
            if (headTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", headTable.Idhead);
            return View(headTable);
        }

        // POST: HeadTables/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Idhead,Time,ConfigurationTable,ConnectedTable")] HeadTable headTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(headTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", headTable.Idhead);
            return View(headTable);
        }

        // GET: HeadTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadTable headTable = db.DvHeadTables.Find(id);
            if (headTable == null)
            {
                return HttpNotFound();
            }
            return View(headTable);
        }

        // POST: HeadTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HeadTable headTable = db.DvHeadTables.Find(id);
            db.DvHeadTables.Remove(headTable);
            db.SaveChanges();
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
