using MebleShop.Models.Entities;
using MebleShop.Models.Entities.OurServices;
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
    public class OurServiceController : Controller
    {
        private FeedBackContext db = new FeedBackContext();

        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        public ActionResult UserServiceIndex()
        {
            return View(db.Services.ToList());
        }

        public ActionResult UserServiceDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Include(s => s.FileServiceDetails).SingleOrDefault(x => x.ServiceId == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                List<FileServiceDetail> fileDetails = new List<FileServiceDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileServiceDetail fileDetail = new FileServiceDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        if (fileDetail.Extension == ".jpg")
                        {
                            fileDetails.Add(fileDetail);

                            var path = Path.Combine(Server.MapPath("~/App_Data/Upload_service/"), fileDetail.Id + fileDetail.Extension);
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

                service.FileServiceDetails = fileDetails;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Include(s => s.FileServiceDetails).SingleOrDefault(x => x.ServiceId == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload_service/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }

        public ActionResult GetImage(string serviceId)
        {
            if (serviceId != null)
            {
                var dir = Server.MapPath("~/App_Data/Upload_service/");
                var path = Path.Combine(dir, serviceId + ".jpg");
                return File(path, "image/jpeg");
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {

                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileServiceDetail fileDetail = new FileServiceDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            ServiceId = service.ServiceId
                        };
                        if (fileDetail.Extension == ".jpg")
                        {
                            var path = Path.Combine(Server.MapPath("~/App_Data/Upload_service/"), fileDetail.Id + fileDetail.Extension);
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
                        TempData["ErrorMessage"] = "Должна быть минимум 1 фотография";
                        return RedirectToAction("Create");
                    }
                }

                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }


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
                FileServiceDetail fileDetail = db.FileServiceDetails.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileServiceDetails.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload_service/"), fileDetail.Id + fileDetail.Extension);
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


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Service service = db.Services.Find(id);
                if (service == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //delete files from the file system

                foreach (var item in service.FileServiceDetails)
                {
                    String path = Path.Combine(Server.MapPath("~/App_Data/Upload_service/"), item.Id + item.Extension);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                db.Services.Remove(service);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public ActionResult PartialService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Include(s => s.FileServiceDetails).SingleOrDefault(x => x.ServiceId == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //public ActionResult Error()
        //{
        //    //ViewBag.Message = TempData["message"];
        //    //ViewBag.Message = "Неправильное расширине файла для загрузки(только .jpg)";
        //    return View();
        //}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}