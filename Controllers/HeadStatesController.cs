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
    public class HeadStatesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: HeadStates
        public ActionResult Index()
        {
            var dvHeadStates = db.DvHeadStates.Include(h => h.DvHead);
            return View(dvHeadStates.ToList());
        }

        // GET: HeadStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadState headState = db.DvHeadStates.Find(id);
            if (headState == null)
            {
                return HttpNotFound();
            }
            return View(headState);
        }

        // GET: HeadStates/Create
        public ActionResult Create()
        {
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress");
            return View();
        }

        // POST: HeadStates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,Idhead,Voltage,Temperature,Status,HeadTime")] HeadState headState)
        {
            if (ModelState.IsValid)
            {
                db.DvHeadStates.Add(headState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", headState.Idhead);
            return View(headState);
        }

        // GET: HeadStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadState headState = db.DvHeadStates.Find(id);
            if (headState == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", headState.Idhead);
            return View(headState);
        }

        // POST: HeadStates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,Idhead,Voltage,Temperature,Status,HeadTime")] HeadState headState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(headState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", headState.Idhead);
            return View(headState);
        }

        // GET: HeadStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadState headState = db.DvHeadStates.Find(id);
            if (headState == null)
            {
                return HttpNotFound();
            }
            return View(headState);
        }

        // POST: HeadStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HeadState headState = db.DvHeadStates.Find(id);
            db.DvHeadStates.Remove(headState);
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
