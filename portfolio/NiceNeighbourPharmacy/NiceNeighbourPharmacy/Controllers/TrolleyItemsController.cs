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
    public class TrolleyItemsController : Controller
    {
        private NNPharmacyModels db = new NNPharmacyModels();

        // GET: TrolleyItems
        public ActionResult Index()
        {
            var trolleyItems = db.TrolleyItems.Include(t => t.Medicine);
            return View(trolleyItems.ToList());
        }

        // GET: TrolleyItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrolleyItem trolleyItem = db.TrolleyItems.Find(id);
            if (trolleyItem == null)
            {
                return HttpNotFound();
            }
            return View(trolleyItem);
        }

        // GET: TrolleyItems/Create
        public ActionResult Create()
        {
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name");
            return View();
        }

        // POST: TrolleyItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,Price,MedicineId,CustomerId")] TrolleyItem trolleyItem)
        {
            if (ModelState.IsValid)
            {
                db.TrolleyItems.Add(trolleyItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", trolleyItem.MedicineId);
            return View(trolleyItem);
        }

        // GET: TrolleyItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrolleyItem trolleyItem = db.TrolleyItems.Find(id);
            if (trolleyItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", trolleyItem.MedicineId);
            return View(trolleyItem);
        }

        // POST: TrolleyItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,Price,MedicineId,CustomerId")] TrolleyItem trolleyItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trolleyItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", trolleyItem.MedicineId);
            return View(trolleyItem);
        }

        // GET: TrolleyItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrolleyItem trolleyItem = db.TrolleyItems.Find(id);
            if (trolleyItem == null)
            {
                return HttpNotFound();
            }
            return View(trolleyItem);
        }

        // POST: TrolleyItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrolleyItem trolleyItem = db.TrolleyItems.Find(id);
            db.TrolleyItems.Remove(trolleyItem);
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
