using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MebleShop.Models.Entities;

namespace MebleShop.Controllers
{
    public class FeedbacksController : Controller
    {
        private FeedBackContext db = new FeedBackContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Feedbacks.ToList());
        }
        [Authorize]
        public ActionResult InfoRead()
        {
            return View(db.Feedbacks.ToList());
        }
        // GET: Feedbacks/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeedId,FirstName,SecondName,Email,PhoneNumber,Details")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.IsRead = false;
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Спасибо за потраченое время на выполнение формы, мы рассмотрим вашу заявку в ближайшее время";
                return RedirectToAction("Index", "MainShop");
            }

            return View(feedback);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public JsonResult ToRead(int id)
        {
            try
            {
                Feedback feedback = db.Feedbacks.Find(id);
                if (feedback == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                feedback.IsRead = true;
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
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
