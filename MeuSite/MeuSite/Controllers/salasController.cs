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
    public class salasController : Controller
    {
        private edbancoEntities db = new edbancoEntities();
        

        // GET: salas
        public ActionResult Index()
        {
            ViewBag.idTemaSala = new SelectList(db.temasala, "idTemaSala", "nome");
            return View(db.sala.ToList());
        }

        // GET: salas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sala sala = db.sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // GET: salas/Create
        public ActionResult Create(int? idtema)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            if (ViewBag.mensagem == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.idTemaSala = new SelectList(db.temasala, "idTemaSala", "nome");
            return View();
        }

        // POST: salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsala,idTemaSala,Nome,Descrecao")] sala sala)
        {
            if (ModelState.IsValid)
            {
                db.sala.Add(sala);
                db.SaveChanges();
                sala atu = new sala();
                foreach (var item in db.sala.ToList())
                {
                    if (item == sala)
                    {
                        atu = item;
                    }
                }
                ViewBag.mensagem = TempData["ID"];
                TempData["ID"] = ViewBag.mensagem;
                usuariosala pessoa = new usuariosala();
                pessoa.idSala = atu.idsala;
                chat espa = new chat();
                espa.idSala = atu.idsala;
                if (TempData["ID"] == null)
                {
                    return RedirectToAction("Entrar", "usuarios");
                }
                else
                {
                    foreach (var item in db.usuario.ToList())
                    {
                        if (ViewBag.mensagem.Equals(item.idusuario))
                        {
                            pessoa.idUsuario = item.idusuario;
                            espa.idUsuario = item.idusuario;
                        }
                    }
                }
                pessoa.proprietario = true;
                pessoa.acessopermitido = true;
                espa.numero = 1;
                espa.mensagem = "!-";
                db.chat.Add(espa);
                db.usuariosala.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index", "chats");
            }

            return View(sala);
        }

        // GET: salas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sala sala = db.sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsala,idTemaSala,Nome,Descrecao")] sala sala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sala);
        }

        // GET: salas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sala sala = db.sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sala sala = db.sala.Find(id);
            db.sala.Remove(sala);
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
