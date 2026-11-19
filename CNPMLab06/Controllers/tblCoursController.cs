using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPMLab06;

namespace CNPMLab06.Controllers
{
    public class tblCoursController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: tblCours
        public ActionResult Index()
        {
            var tblCourses = db.tblCourses.Include(t => t.tblDepartment).Include(t => t.tblInstructor);
            return View(tblCourses.ToList());
        }

        // GET: tblCours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCours tblCours = db.tblCourses.Find(id);
            if (tblCours == null)
            {
                return HttpNotFound();
            }
            return View(tblCours);
        }

        // GET: tblCours/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.tblDepartments, "DepartmentID", "DepartmentName");
            ViewBag.InstructorID = new SelectList(db.tblInstructors, "InstructorID", "InstructorName");
            return View();
        }

        // POST: tblCours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,CourseName,Credits,DepartmentID,InstructorID")] tblCours tblCours)
        {
            if (ModelState.IsValid)
            {
                db.tblCourses.Add(tblCours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.tblDepartments, "DepartmentID", "DepartmentName", tblCours.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.tblInstructors, "InstructorID", "InstructorName", tblCours.InstructorID);
            return View(tblCours);
        }

        // GET: tblCours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCours tblCours = db.tblCourses.Find(id);
            if (tblCours == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.tblDepartments, "DepartmentID", "DepartmentName", tblCours.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.tblInstructors, "InstructorID", "InstructorName", tblCours.InstructorID);
            return View(tblCours);
        }

        // POST: tblCours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CourseName,Credits,DepartmentID,InstructorID")] tblCours tblCours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.tblDepartments, "DepartmentID", "DepartmentName", tblCours.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.tblInstructors, "InstructorID", "InstructorName", tblCours.InstructorID);
            return View(tblCours);
        }

        // GET: tblCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCours tblCours = db.tblCourses.Find(id);
            if (tblCours == null)
            {
                return HttpNotFound();
            }
            return View(tblCours);
        }

        // POST: tblCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCours tblCours = db.tblCourses.Find(id);
            db.tblCourses.Remove(tblCours);
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
