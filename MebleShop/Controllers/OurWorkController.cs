using MebleShop.Models.Entities;
using MebleShop.Models.Entities.OurWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MebleShop.Controllers
{
    public class OurWorkController : Controller
    {
        private FeedBackContext db = new FeedBackContext();
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Works.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Work work)
        {
            if (ModelState.IsValid)
            {
                List<FileWorkDetail> fileDetails = new List<FileWorkDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileWorkDetail fileDetail = new FileWorkDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        if (fileDetail.Extension == ".jpg")
                        {
                            fileDetails.Add(fileDetail);

                            var path = Path.Combine(Server.MapPath("~/App_Data/Upload_work/"), fileDetail.Id + fileDetail.Extension);
                            file.SaveAs(path);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Неправильное расширине файла для загрузки(только .jpg)";
                            return RedirectToAction("Create");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Должна быть минимум 1 фотография";
                        return RedirectToAction("Create");
                    }
                }

                work.FileWorkDetails = fileDetails;
                db.Works.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(work);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Include(s => s.FileWorkDetails).SingleOrDefault(x => x.WorkId == id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        [Authorize]
        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload_work/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }

        public ActionResult GetImage(string workId)
        {
            if (workId != null)
            {
                var dir = Server.MapPath("~/App_Data/Upload_work/");
                var path = Path.Combine(dir, workId + ".jpg"); 
                return File(path, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
        //public ActionResult GetImagePartial(string workId)
        //{
        //    if (workId != null)
        //    {
        //        var dir = Server.MapPath("~/App_Data/Upload_work/");
        //        var path = Path.Combine(dir, workId + ".jpg");
        //        return File(path, "image/jpeg");
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Work work)
        {
            if (ModelState.IsValid)
            {

                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (work.Description != null) // rewrite
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            FileWorkDetail fileDetail = new FileWorkDetail()
                            {
                                FileName = fileName,
                                Extension = Path.GetExtension(fileName),
                                Id = Guid.NewGuid(),
                                WorkId = work.WorkId
                            };
                            if (fileDetail.Extension == ".jpg")
                            {
                                var path = Path.Combine(Server.MapPath("~/App_Data/Upload_work/"), fileDetail.Id + fileDetail.Extension);
                                file.SaveAs(path);
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Неправильное расширине файла для загрузки(только .jpg)";
                                return RedirectToAction("Edit");
                            }
                            db.Entry(fileDetail).State = EntityState.Added;
                        }
                        else
                        {
                            //TempData["ErrorMessage"] = "Должна быть минимум 1 фотография";
                            //return RedirectToAction("Edit");
                        }
                    }                   
                }

                db.Entry(work).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(work);
        }

        [Authorize]
        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileWorkDetail fileDetail = db.FileWorkDetails.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileWorkDetails.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload_work/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Work work = db.Works.Find(id);
                if (work == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //delete files from the file system

                foreach (var item in work.FileWorkDetails)
                {
                    String path = Path.Combine(Server.MapPath("~/App_Data/Upload_work/"), item.Id + item.Extension);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                db.Works.Remove(work);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult PartialWork(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Include(s => s.FileWorkDetails).SingleOrDefault(x => x.WorkId == id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        public ActionResult UserWorkIndex()
        {
            return View(db.Works.ToList());
        }
        public ActionResult UserWorkDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Include(s => s.FileWorkDetails).SingleOrDefault(x => x.WorkId == id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}