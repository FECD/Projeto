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
    public class arquivotarefasController : Controller
    {
        private Model6 db = new Model6();

        // GET: arquivotarefas
        //public ActionResult Index()
        //{
        //    return View(db.arquivotarefa.ToList());
        //}

        //// GET: arquivotarefas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    arquivotarefa arquivotarefa = db.arquivotarefa.Find(id);
        //    if (arquivotarefa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(arquivotarefa);
        //}

        //// GET: arquivotarefas/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: arquivotarefas/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Create([Bind(Include = "idArquivoTarefa,idArquivoBiblioteca")] arquivotarefa arquivotarefa)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.arquivotarefa.Add(arquivotarefa);
        ////        db.SaveChanges();
        ////        return RedirectToAction("Index");
        ////    }

        ////    return View(arquivotarefa);
        ////}

        //// GET: arquivotarefas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    arquivotarefa arquivotarefa = db.arquivotarefa.Find(id);
        //    if (arquivotarefa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(arquivotarefa);
        //}

        //// POST: arquivotarefas/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idArquivoTarefa,idArquivoBiblioteca")] arquivotarefa arquivotarefa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(arquivotarefa).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(arquivotarefa);
        //}

        //// GET: arquivotarefas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    arquivotarefa arquivotarefa = db.arquivotarefa.Find(id);
        //    if (arquivotarefa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(arquivotarefa);
        //}

        //// POST: arquivotarefas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    arquivotarefa arquivotarefa = db.arquivotarefa.Find(id);
        //    db.arquivotarefa.Remove(arquivotarefa);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
