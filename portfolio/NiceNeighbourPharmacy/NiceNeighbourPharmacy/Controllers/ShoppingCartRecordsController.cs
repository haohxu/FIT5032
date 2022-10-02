using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceNeighbourPharmacy.Models;

namespace NiceNeighbourPharmacy.Controllers
{
    public class ShoppingCartRecordsController : Controller
    {
        private NNPharmacy_Models db = new NNPharmacy_Models();

        // GET: ShoppingCartRecords
        public ActionResult Index()
        {
            var shoppingCartRecords = db.ShoppingCartRecords.Include(s => s.Customer).Include(s => s.Medicine);
            return View(shoppingCartRecords.ToList());
        }

        // GET: ShoppingCartRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartRecord shoppingCartRecord = db.ShoppingCartRecords.Find(id);
            if (shoppingCartRecord == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartRecord);
        }

        // GET: ShoppingCartRecords/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName");
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name");
            return View();
        }

        // POST: ShoppingCartRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,MedicineId,CustomerId")] ShoppingCartRecord shoppingCartRecord)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCartRecords.Add(shoppingCartRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", shoppingCartRecord.CustomerId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", shoppingCartRecord.MedicineId);
            return View(shoppingCartRecord);
        }

        // GET: ShoppingCartRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartRecord shoppingCartRecord = db.ShoppingCartRecords.Find(id);
            if (shoppingCartRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", shoppingCartRecord.CustomerId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", shoppingCartRecord.MedicineId);
            return View(shoppingCartRecord);
        }

        // POST: ShoppingCartRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,MedicineId,CustomerId")] ShoppingCartRecord shoppingCartRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCartRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", shoppingCartRecord.CustomerId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", shoppingCartRecord.MedicineId);
            return View(shoppingCartRecord);
        }

        // GET: ShoppingCartRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartRecord shoppingCartRecord = db.ShoppingCartRecords.Find(id);
            if (shoppingCartRecord == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartRecord);
        }

        // POST: ShoppingCartRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCartRecord shoppingCartRecord = db.ShoppingCartRecords.Find(id);
            db.ShoppingCartRecords.Remove(shoppingCartRecord);
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
