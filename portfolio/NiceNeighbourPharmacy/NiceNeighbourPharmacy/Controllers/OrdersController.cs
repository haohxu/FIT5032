using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NiceNeighbourPharmacy.Models;
using NiceNeighbourPharmacy.Utils;

namespace NiceNeighbourPharmacy.Controllers
{
    [RequireHttps]
    [Authorize]
    public class OrdersController : Controller
    {
        private NNPharmacyModels db = new NNPharmacyModels();

        // GET: Orders
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            IQueryable<Order> orders = null;
            if (User.IsInRole("Customer"))
            {
                orders = db.Orders
                    .OrderByDescending(t => t.Status)
                    .Where(t => t.CustomerId == currentUserId);
            }
            else if (User.IsInRole("Pharmacist"))
            {
                orders = db.Orders
                    .OrderByDescending(t => t.CollectDateTime);
            }
            else
            {
                orders = db.Orders
                    .OrderByDescending(t => t.CollectDateTime);
            }
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            
            // get current user
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            // get orderDetails
            var orderDetails = db.OrderDetails.Where(t => t.OrderId == order.Id);

            DetailedOrderViewModel dovm = new DetailedOrderViewModel();
            dovm.TheOrder = order;
            dovm.CustomerName = currentUser.LastName + ", " + currentUser.FirstName;
            dovm.OrderDetails = orderDetails;

            return View(dovm);
        }

        // Start - 
        // GET: Orders/SendGroupEmail
        public ActionResult SendGroupEmail()
        {
            
            var orders = db.Orders.Where(t =>
                t.Status == "Ready to Collect"
            );

            SendGroupEmailViewModel sendGroupEmailViewModel = new SendGroupEmailViewModel();
            sendGroupEmailViewModel.Orders = orders;

            return View(sendGroupEmailViewModel);
        }

        // POST: Orders/SendGroupEmail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendGroupEmail(
            [Bind(Include = "Subject,Contents")] SendGroupEmailViewModel model, 
            HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {

                string serverFolderPath = Server.MapPath("~/Uploads/" + User.Identity.GetUserId() + "/");
                // string serverFolderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(serverFolderPath))
                    Directory.CreateDirectory(serverFolderPath);
                string uniqueFileName = string.Format(@"{0}", Guid.NewGuid()) + Path.GetExtension(postedFile.FileName);
                string fullFilePath = serverFolderPath + uniqueFileName;

                postedFile.SaveAs(fullFilePath);

                var manager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(
                        new ApplicationDbContext()));

                var orders = db.Orders.Where(t =>
                    t.Status == "Ready to Collect"
                );


                try
                {
                    foreach (var order in orders)
                    {
                        string toEmail = manager.FindById(order.CustomerId).Email;
                        string subject = model.Subject;
                        string contents = model.Contents;

                        SendEmailUtil.Execute(subject, contents, toEmail, fullFilePath);
                    }
                }
                catch
                {
                    return View();
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }
        // End -----

        //// GET: Orders/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,TotalPrice,Status,CollectDateTime,CustomerId,PharmacistId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(order);
        //}

        // GET: Orders/Edit/5
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Edit([Bind(Include = "Id,TotalPrice,Status,CollectDateTime,CustomerId,PharmacistId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
