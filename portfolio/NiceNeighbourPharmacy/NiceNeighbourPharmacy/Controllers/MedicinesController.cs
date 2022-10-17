﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NiceNeighbourPharmacy.Models;

namespace NiceNeighbourPharmacy.Controllers
{
    [Authorize]
    public class MedicinesController : Controller
    {
        private NNPharmacyModels db = new NNPharmacyModels();

        // GET: Medicines
        public ActionResult Index()
        {
            return View(db.Medicines.ToList());
        }

        // Start - Add to Trolley
        // GET: Medicines/AddToTrolley/5
        public ActionResult AddToTrolley(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/AddToTrolley/5
        [HttpPost, ActionName("AddToTrolley")]
        [ValidateAntiForgeryToken]
        public ActionResult AddToTrolleyConfirmed(int id)
        {
            var currentCustomerId = User.Identity.GetUserId();
            
            Medicine medicine = db.Medicines.Find(id);
            var trolleyItems = db.TrolleyItems.Where(t =>
                t.CustomerId == currentCustomerId);
            TrolleyItem toUpdateItem = trolleyItems.FirstOrDefault(t =>
                t.MedicineId == medicine.Id);
            
            if (toUpdateItem == null)
            {
                TrolleyItem item = new TrolleyItem();
                item.CustomerId = currentCustomerId;
                item.Quantity = 1;
                item.Price = medicine.Price;
                item.MedicineId = medicine.Id;
                db.TrolleyItems.Add(item);

                db.SaveChanges();
            }
            else
            {
                toUpdateItem.Quantity += 1;
                db.Entry(toUpdateItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // End -----

        // GET: Medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Medicines/Create
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Category,Price,NumberOfStock,AvgRatings,Notes")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicine);
        }

        // GET: Medicines/Edit/5
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Category,Price,NumberOfStock,AvgRatings,Notes")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicine);
        }


        // GET: Medicines/Delete/5
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
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
