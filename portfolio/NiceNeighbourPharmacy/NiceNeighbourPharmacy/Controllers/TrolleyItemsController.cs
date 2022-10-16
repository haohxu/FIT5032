using System;
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
    [Authorize(Roles = "Customer")]
    public class TrolleyItemsController : Controller
    {
        private NNPharmacyModels db = new NNPharmacyModels();

        // GET: TrolleyItems
        public ActionResult Index()
        {
            // var trolleyItems = db.TrolleyItems.Include(t => t.Medicine);
            var currentUserId = User.Identity.GetUserId();

            var trolleyItems = db.TrolleyItems.Where(t =>
                t.CustomerId == currentUserId
            );
            return View(trolleyItems.ToList());
        }

        // Start -

        // GET: TrolleyItems/Confirm
        public ActionResult Confirm()
        {
            // var trolleyItems = db.TrolleyItems.Include(t => t.Medicine);
            var currentUserId = User.Identity.GetUserId();

            var trolleyItems = db.TrolleyItems.Where(t =>
                t.CustomerId == currentUserId
            );

            ConfirmOrderViewModel confirmOrderViewModel = new ConfirmOrderViewModel();
            confirmOrderViewModel.TrolleyItems = trolleyItems;

            // return View(trolleyItems.ToList());
            return View(confirmOrderViewModel);
        }

        // POST: TrolleyItems/Confirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "CollectDateTime")] ConfirmOrderViewModel model)
        {
            DateTime collectDateTime = model.CollectDateTime;
            
            // var currentUser = 
            var currentUserId = User.Identity.GetUserId();

            var items = db.TrolleyItems.Where(s =>
                s.CustomerId == currentUserId
            ).ToList();

            decimal totalPrice = 0;
            foreach (var item in items)
            {
                if (item.Price != null && item.Quantity != null)
                {
                    totalPrice = (decimal)(totalPrice + (item.Price * item.Quantity));
                }
                else { continue; }
            }

            // Process: Order entity
            Order newOrder = new Order();
            newOrder.Status = "Ready to Collect";
            newOrder.CustomerId = currentUserId;
            newOrder.CollectDateTime = collectDateTime;
            newOrder.TotalPrice = totalPrice;
            Order insertedOrder = db.Orders.Add(newOrder);
            db.SaveChanges();

            // Process: Event entity
            Event newEvent = new Event();
            newEvent.OrderId = insertedOrder.Id;
            newEvent.Title = "Collect Medicines";
            newEvent.Description = currentUserId;
            newEvent.TimeStart = collectDateTime;
            newEvent.TimeEnd = collectDateTime.AddMinutes(19.5);
            Event insertedEvent = db.Events.Add(newEvent);
            db.SaveChanges();

            // Process: OrderDetail and Rating entities
            

            foreach (var item in items)
            {
                OrderDetail newDetail = new OrderDetail();
                newDetail.OrderId = insertedOrder.Id;
                newDetail.MedicineId = item.MedicineId;
                newDetail.Price = item.Price;
                newDetail.Quantity = item.Quantity;
                OrderDetail insertedDetail = db.OrderDetails.Add(newDetail);
                db.SaveChanges();

                Rating newRating = new Rating();
                newRating.OrderDetailId = insertedDetail.Id;
                newRating.RatingStatus = "Ready to Rate";
                Rating insertedRating = db.Ratings.Add(newRating);
                db.SaveChanges();
            }

            foreach (var item in items)
            {
                TrolleyItem trolleyItem = db.TrolleyItems.Find(item.Id);
                db.TrolleyItems.Remove(trolleyItem);
                db.SaveChanges();
            }

            db.SaveChanges();


            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    db.TrolleyItems.Add(trolleyItem);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", trolleyItem.MedicineId);
            //return View(trolleyItem);
        }
        // End -----

        // GET: TrolleyItems/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TrolleyItem trolleyItem = db.TrolleyItems.Find(id);
        //    if (trolleyItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(trolleyItem);
        //}

        //// GET: TrolleyItems/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name");
        //    return View();
        //}

        //// POST: TrolleyItems/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Quantity,Price,MedicineId,CustomerId")] TrolleyItem trolleyItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TrolleyItems.Add(trolleyItem);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", trolleyItem.MedicineId);
        //    return View(trolleyItem);
        //}

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
