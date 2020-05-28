using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektOsoby.DAL;
using ProjektOsoby.Models;

namespace ProjektOsoby.Controllers
{
    public class PeopleController : Controller
    {
        private MPSContext db = new MPSContext();

        // GET: People
        public ActionResult Index(string sortOrder)
        {
            ViewBag.S = sortOrder == "s_asc" ? "s_desc" : "s_asc";
            ViewBag.D = sortOrder == "d_asc" ? "d_desc" : "d_asc";
            var people = db.Person.Where(s => s.IsActual == true).ToList();


            switch (sortOrder)
            {
                case "s_asc":
                    people = people.OrderBy(s => s.Sex).Where(s => s.IsActual == true).ToList();
                    break;
                case "s_desc":
                    people = people.OrderByDescending(s => s.Sex).Where(s => s.IsActual == true).ToList();
                    break;
                case "d_asc":
                    people = people.OrderBy(s => s.Added).Where(s => s.IsActual == true).ToList();
                    break;
                case "d_desc":
                    people = people.OrderByDescending(s => s.Added).Where(s => s.IsActual == true).ToList();
                    break;
            }



            return View(people);
        }

        [Authorize(Roles = "Admin")]
        // GET: People
        public ActionResult Index2(string sortOrder)
        {
            ViewBag.S = sortOrder == "s_asc" ? "s_desc" : "s_asc";
            ViewBag.D = sortOrder == "d_asc" ? "d_desc" : "d_asc";
            var people = from i in db.Person select i;
            switch (sortOrder)
            {
                case "s_asc":
                    people = people.OrderBy(s => s.Sex);
                    break;
                case "s_desc":
                    people = people.OrderByDescending(s => s.Sex);
                    break;
                case "d_asc":
                    people = people.OrderBy(s => s.Added);
                    break;
                case "d_desc":
                    people = people.OrderByDescending(s => s.Added);
                    break;
            }
            return View(people);
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [Authorize(Roles = "Admin")]
        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.DateTime = DateTime.UtcNow;
            return View();
        }

        // POST: People/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Image,Description,Sex,Added,IsActual")] Person person)
        {
            ViewBag.DateTime = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["image"];
                if (file != null && file.ContentLength > 0)
                {
                    person.Image = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + person.Image);
                }

                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            return View(person);
        }

        // GET: People/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Image,Description,Sex,Added,IsActual")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            return View(person);
        }

        // GET: People/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index2");
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
