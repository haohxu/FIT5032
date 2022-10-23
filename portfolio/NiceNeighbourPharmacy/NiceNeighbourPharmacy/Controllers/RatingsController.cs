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
    [RequireHttps]
    [Authorize(Roles = "Customer")]
    public class RatingsController : Controller
    {
        private NNPharmacyModels db = new NNPharmacyModels();

        // GET: Ratings
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var ratings = db.Ratings
                .OrderByDescending(t => t.RatingStatus)
                .Where(t => t.OrderDetail.Order.CustomerId == currentUserId);
            return View(ratings.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        //public ActionResult Create()
        //{
        //    ViewBag.OrderDetailId = new SelectList(db.OrderDetails, "Id", "Id");
        //    return View();
        //}

        //// POST: Ratings/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,RatingScore,RatingComment,RatingStatus,OrderDetailId")] Rating rating)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Ratings.Add(rating);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.OrderDetailId = new SelectList(db.OrderDetails, "Id", "Id", rating.OrderDetailId);
        //    return View(rating);
        //}

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderDetailId = new SelectList(db.OrderDetails, "Id", "Id", rating.OrderDetailId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RatingScore,RatingComment,RatingStatus,OrderDetailId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                var currentOrderDetail = db.OrderDetails.Find(rating.OrderDetailId);
                var currentMedicine = db.Medicines.Find(currentOrderDetail.MedicineId);

                if (rating.RatingScore != null || rating.RatingComment != null) 
                {
                    rating.RatingStatus = "Rated";    
                }
                else 
                {
                    rating.RatingStatus = "Ready to Rate";
                }
                
                int theMedicineId = currentMedicine.Id;
                decimal? theRatingScore = rating.RatingScore;

                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();

                // Start - Add Rating Function
                Medicine medicineToUpdate = db.Medicines.FirstOrDefault(m => 
                    m.Id == theMedicineId
                );
                decimal? avgScore = 0;
                if (medicineToUpdate.AvgRatings == null)
                {
                    avgScore = theRatingScore;
                    
                }
                else
                {
                    var ratings = db.Ratings.Where(s =>
                        s.OrderDetail.MedicineId == medicineToUpdate.Id &&
                        s.RatingScore != null
                    );
                    decimal? sumScore = 0;
                    foreach (var item in ratings)
                    {
                        sumScore = sumScore + item.RatingScore;
                    }
                    avgScore = sumScore / ratings.Count();
                    
                }
                medicineToUpdate.AvgRatings = avgScore;
                db.Entry(medicineToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                // End -----

                return RedirectToAction("Index");
            }
            ViewBag.OrderDetailId = new SelectList(db.OrderDetails, "Id", "Id", rating.OrderDetailId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
