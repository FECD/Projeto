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
    public class usuariosController : Controller
    {
        private edbancoEntities db = new edbancoEntities();


        public ActionResult Entrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Entrar([Bind(Include = "email,senha")] usuario usuario)
        {
            usuario useencontrado = db.usuario.ToList().Find(s => s.email == usuario.email);
            if (useencontrado != null)
            {
                if (useencontrado.senha == usuario.senha)
                {
                    usuario novo = useencontrado;
                    novo.conexao = true;
                    db.Entry(novo).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Email"] = novo.email;
                    return RedirectToAction("Index", "Home");
                }
            }
            else{
                return RedirectToAction("Entrar", "usuarios");
            }
            //foreach (var item in )
            //{
            //    if (item.email == usuario.email)
            //    {
            //        if (item.senha == usuario.senha)
            //        {
            //            usuario novo = item;
            //            novo.conexao = true;
            //            db.Entry(novo).State = EntityState.Modified;
            //            db.SaveChanges();
            //            TempData["Email"] = item.email;
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}

            return View();
        }
        public ActionResult Sair()
        {
            ViewBag.Idusuario = TempData["ID"];
            TempData.Clear();
            usuario atual = db.usuario.ToList().Find(s => s.idusuario == ViewBag.Idusuario);
            if (atual != null)
            {
                atual.conexao = false;
                db.Entry(atual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Entrar", "usuarios");
            }
            else
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            
        }
        public ActionResult Perfil()
        {
            if (TempData["Email"] == null)
            {
                return RedirectToAction("Entrar");
            }
            usuario pessoa = new usuario();
            foreach (var item in db.usuario.ToList())
            {
                if (item.email == TempData["Email"].ToString())
                {
                    pessoa = item;
                }
            }
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar");
            }
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            return View(pessoa);
        }
        // GET: usuarios
        public ActionResult Index()
        {
            return View(db.usuario.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "idusuario,nome,email,senha")] usuario usuario)
        {
            usuario existente = db.usuario.ToList().Find(item => item.email == usuario.email);
            if (existente != null)
            {
                ViewBag.aviso= "Esse Email está registrado em outra conta";
                return RedirectToAction("Create", "usuarios");
            }
            else
            {
                //if (ModelState.IsValid)
                
                    usuario.conexao = false;
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Entrar", "usuarios");
                
            }

            //return View(usuario);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idusuario,nome,email,senha")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.conexao = true;
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Perfil", "usuarios");
            }
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
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
