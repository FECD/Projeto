using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeuSite.Models;
using System.IO;

namespace MeuSite.Controllers
{
    public class arquivobibliotecasController : Controller
    {
        private edbancoEntities db = new edbancoEntities();

        // GET: arquivobibliotecas
        public ActionResult Index()
        {
            return View(db.arquivobiblioteca.ToList());
        }

        // GET: arquivobibliotecas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
            if (arquivobiblioteca == null)
            {
                return HttpNotFound();
            }
            return View(arquivobiblioteca);
        }

        // GET: arquivobibliotecas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: arquivobibliotecas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idarquivoBiblioteca,idTemaSala,nome,conteudo,privacidade")] arquivobiblioteca arquivobiblioteca, HttpPostedFileBase file)
        {
            ViewBag.id = TempData["IDSALA"];
            sala sala = db.sala.ToList().Find(item => item.idsala == ViewBag.id);
            TempData["IDSALA"] = ViewBag.id;
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName.ToString());
                var path = Path.Combine(Server.MapPath("~/Arquivos/Biblioteca"), fileName);
                file.SaveAs(path);
                path = Url.Content(Path.Combine("~/Arquivos/Biblioteca", fileName));
                arquivobiblioteca.conteudo = path;
                arquivobiblioteca.idTemaSala = sala.idTemaSala;
                db.arquivobiblioteca.Add(arquivobiblioteca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arquivobiblioteca);
        }

        // GET: arquivobibliotecas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
            if (arquivobiblioteca == null)
            {
                return HttpNotFound();
            }
            return View(arquivobiblioteca);
        }

        // POST: arquivobibliotecas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idarquivoBiblioteca,idTemaSala,nome,conteudo")] arquivobiblioteca arquivobiblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arquivobiblioteca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arquivobiblioteca);
        }

        // GET: arquivobibliotecas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
            if (arquivobiblioteca == null)
            {
                return HttpNotFound();
            }
            return View(arquivobiblioteca);
        }

        // POST: arquivobibliotecas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
            db.arquivobiblioteca.Remove(arquivobiblioteca);
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
