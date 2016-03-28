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
    public class tarefasController : Controller
    {
        private bancofcEntities db = new bancofcEntities();

        // GET: tarefas
        //public ActionResult Index()
        //{
        //    return View(db.tarefa.ToList());
        //}

        //// GET: tarefas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tarefa tarefa = db.tarefa.Find(id);
        //    if (tarefa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tarefa);
        //}

        // GET: tarefas/Create
        public ActionResult Create()
        {
            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;

            ViewBag.sala_destino = TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            ViewBag.usuario = TempData["USUARIO+USER"];
            ViewBag.privilegios = TempData["PRIVILEGIOS"];

            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;
            return View();
        }

        // POST: tarefas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idtarefa,titulo,descricao")] tarefa tarefa)
        {
            //System.Date dataBancoMysql = Convert.ToDateTime(dataEntrega.ToString()).ToString("yyyy/MM/dd");
            //tarefa.dataEntrega.ToUniversalTime();
            ViewBag.idSala = TempData["IDSALA"];
            TempData["IDSALA"] = ViewBag.idSala;
            sala k = db.sala.ToList().Find(s=> s.idsala == ViewBag.idSala);
            tarefa.sala_idsala = k.idsala;
            tarefa.sala_temasala_idtemasala = k.temasala_idtemasala;
            tarefa.entregue = false;
            //tarefa.dataEntrega = dataBancoMysql.ToString();
            if (ModelState.IsValid)
            {
                db.tarefa.Add(tarefa);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(tarefa);
        }

        //// GET: tarefas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tarefa tarefa = db.tarefa.Find(id);
        //    if (tarefa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tarefa);
        //}

        //// POST: tarefas/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idtarefa,idSala,titulo,dataEntrega,entregue")] tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarefa).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        //// GET: tarefas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tarefa tarefa = db.tarefa.Find(id);
        //    if (tarefa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tarefa);
        //}

        //// POST: tarefas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tarefa tarefa = db.tarefa.Find(id);
        //    db.tarefa.Remove(tarefa);
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
