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
using DivisionWebGlobal.Models;
using System.Data.Entity.Infrastructure;

namespace DivisionWebGlobal.Controllers.Data
{
    [Authorize]
    public class ConfigsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Configs
        public ActionResult Index()
        {
            return View(db.DvHeads.ToList());
        }

        // GET: Configs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.DvHeads.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        public ActionResult Process(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.DvHeads.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Events", new { idHead = id });
            // PRG pattern
        }

        // GET: Configs/Create
        public ActionResult Create()
        {
            Config config = new Config()
            {
                Ipaddress = "192.168.0.1",
                Port = 5014
            };
            config.IsNeedToSendDisplay = new Models.Graphics.CheckBoxListItem
            {
                Display = "Рассылку на email осуществлять",
                IsChecked = config.IsNeedToSend
            };
            return View(config);
        }

        // POST: Configs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ipaddress,Port,SerialNumber,Password,IsNeedToSend,Comment,OwnerName,PlaceAddress")] Config config,
            [Bind(Include = "IdAddress,City,Mkrn,Latitude,Longitude")] Address address, [Bind(Include = "IdOwner, Fio, Email")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Address _address = address;
                    config.Address = address;
                    config.Owner = owner;
                    db.Addresses.Add(address);
                    db.Owners.Add(owner);
                    db.SaveChanges();

                    db.DvHeads.Add(config);
                    db.SaveChanges();
                } catch (DbUpdateException ex)
                {
                    ;
                }
                return RedirectToAction("Index");
            }

            return View(config);
        }

        // GET: Configs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.DvHeads.Find(id);

            if (config == null)
            {
                return HttpNotFound();
            }
            config.IsNeedToSendDisplay = new Models.Graphics.CheckBoxListItem
            {
                Display = "Рассылку на email осуществлять",
                IsChecked = config.IsNeedToSend
            };

            return View(config);
        }

        // POST: Configs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ipaddress,Port,SerialNumber,Password,IsNeedToSendDisplay,Comment,OwnerName,PlaceAddress")] Config config,
            [Bind(Include = "IdAddress,City,Mkrn,Latitude,Longitude")] Address address, [Bind(Include = "IdOwner, Fio, Email")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO check security restrainment
                    //var _config = db.DvHeads.Find(config.Id);

                    var _address = db.Addresses.Find(address.IdAddress);
                    var _owner = db.Owners.Find(owner.IdOwner);
                    // не обязательно
                    _address.City = address.City;
                    _address.Mkrn = address.Mkrn;
                    _address.Latitude = address.Latitude;
                    _address.Longitude = address.Longitude;

                    db.Entry(_address).State = EntityState.Modified;
                    db.SaveChanges();
                    // не обязательно
                    _owner.Fio = owner.Fio;
                    _owner.Email = owner.Email;
                    db.Entry(_owner).State = EntityState.Modified;
                    db.SaveChanges();


                    // Id,Ipaddress,Port,SerialNumber,Password,NeedToSend,Comment,OwnerName,PlaceAddress
                    var _config = db.DvHeads.Find(config.Id);
                    _config.Owner = _owner;
                    _config.Address = _address;

                    _config.SerialNumber = config.SerialNumber;
                    _config.Port = config.Port;
                    _config.Ipaddress = config.Ipaddress;
                    _config.Password = config.Password;
                    _config.IsNeedToSend = config.IsNeedToSendDisplay.IsChecked;
                    _config.Comment = config.Comment;
                    _config.OwnerName = config.OwnerName;
                    _config.PlaceAddress = config.PlaceAddress;

                    //config.Address = _address;
                    //config.Owner = _owner;
                    //db.Entry(config).State = EntityState.Modified;

                    db.Entry(_config).State = EntityState.Modified;
                    db.SaveChanges();

                } catch (DbUpdateException ex)
                {
                    // todo: use almah for remote logs
                    ;
                }
                return RedirectToAction("Index");
            }
            return View(config);
        }

        // GET: Configs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.DvHeads.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Config config = db.DvHeads.Find(id);
            db.DvHeads.Remove(config);
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
