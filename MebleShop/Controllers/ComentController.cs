using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MebleShop.Models.Entities;
using MebleShop.Models.Entities.Comment;

namespace MebleShop.Controllers
{
    public class ComentController : Controller
    {
        private FeedBackContext db = new FeedBackContext();
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Coments.ToListAsync());
        }
        public ActionResult PartialIndex()
        {
            return View(db.Coments.ToList());
        }
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coment coment = await db.Coments.FindAsync(id);
            if (coment == null)
            {
                return HttpNotFound();
            }
            return View(coment);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ComentId,FirstName,PhoneNumber,Details")] Coment coment)
        {
            if (ModelState.IsValid)
            {
                coment.TimeCreated = DateTime.Now;
                db.Coments.Add(coment);
                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Спасибо за Ваш отзыв";
                return RedirectToAction("Create");
            }

            return View(coment);
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coment coment = await db.Coments.FindAsync(id);
            if (coment == null)
            {
                return HttpNotFound();
            }
            return View(coment);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ComentId,FirstName,PhoneNumber,Details")] Coment coment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coment);
        }

        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coment coment = await db.Coments.FindAsync(id);
            if (coment == null)
            {
                return HttpNotFound();
            }
            return View(coment);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Coment coment = await db.Coments.FindAsync(id);
            db.Coments.Remove(coment);
            await db.SaveChangesAsync();
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
