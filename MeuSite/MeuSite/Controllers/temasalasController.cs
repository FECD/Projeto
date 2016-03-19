using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeuSite.Models;

namespace MeuSite.Controllers
{
    public class temasalasController : Controller
    {
        private edbancoEntities db = new edbancoEntities();

        // GET: temasalas
        public ActionResult Index()
        {
            return View(db.temasala.ToList());
        }

        // GET: temasalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temasala temasala = db.temasala.Find(id);
            if (temasala == null)
            {
                return HttpNotFound();
            }
            return View(temasala);
        }

        // GET: temasalas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: temasalas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTemaSala,nome")] temasala temasala)
        {
            if (ModelState.IsValid)
            {
                db.temasala.Add(temasala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(temasala);
        }

        // GET: temasalas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temasala temasala = db.temasala.Find(id);
            if (temasala == null)
            {
                return HttpNotFound();
            }
            return View(temasala);
        }

        // POST: temasalas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTemaSala,nome")] temasala temasala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temasala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temasala);
        }

        // GET: temasalas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temasala temasala = db.temasala.Find(id);
            if (temasala == null)
            {
                return HttpNotFound();
            }
            return View(temasala);
        }

        // POST: temasalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            temasala temasala = db.temasala.Find(id);
            db.temasala.Remove(temasala);
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
