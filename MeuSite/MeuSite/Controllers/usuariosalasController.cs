using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeuSite.Models;
using MeuSite.Classes;

namespace MeuSite.Controllers
{
    public class usuariosalasController : Controller
    {
        private edbancoEntities db = new edbancoEntities();

        // GET: usuariosalas
        public ActionResult Salas(int id, int usu, Boolean gerencia)
        {

            ViewBag.acesso = TempData["Acesso"];
            TarefaChat lista = new TarefaChat();
            List<chat> listaCH = new List<chat>();
            List<tarefa> listaTa = new List<tarefa>();
            if (id != null)
            {
                listaCH = db.chat.ToList().FindAll(item => item.idSala == id);
                listaTa = db.tarefa.ToList().FindAll(item => item.idSala == id);
            }
            //foreach (var item in db.chat.ToList().)
            //{
            //    if (item.idSala == id)
            //    {
            //        listaCH.Add(item);
            //    }
            //}

            lista.listatarefas = listaTa;
            lista.listachat = listaCH;
            lista.idSala = id;
            ViewBag.id = id;
            ViewBag.usuario = usu;
            TempData["Acesso"] = ViewBag.acesso;
            return View(lista);
        }
        public ActionResult Tarefas(int id)
        {
            TempData["IDSALA"] = id;
            return RedirectToAction("Create", "tarefas");
        }
        public ActionResult Arquivos(int id)
        {
            TempData["IDSALA"] = id;
            return RedirectToAction("Create", "arquivobibliotecas");
        }
        public ActionResult SuasSalas()
        {
            ViewBag.acesso = TempData["Acesso"];
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            if (ViewBag.mensagem == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            List<sala> lista = new List<sala>();
            foreach (var item in db.usuariosala.ToList())
            {
                if (TempData["ID"].Equals(item.idUsuario))
                {
                    foreach (var sala in db.sala.ToList())
                    {
                        if (item.idSala == sala.idsala)
                        {
                            lista.Add(sala);
                        }
                    }
                }
            }

            TempData["ID"] = ViewBag.mensagem;
            TempData["Acesso"] = ViewBag.acesso;
            return View(lista);
        }
        public ActionResult CriarSala()
        {
            ViewBag.id = TempData["ID"];
            TempData["ID"] = ViewBag.id;
            usuario objuser = db.usuario.Find(ViewBag.id);
            if (objuser.conexao == true){
                return RedirectToAction("Create", "salas");
            }
            else
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            
        }
        public ActionResult Pesquisa()
        {
            ViewBag.id = TempData["ID"];
            TempData["ID"] = ViewBag.id;
            usuario objuser = db.usuario.Find(ViewBag.id);
            if (objuser.conexao == true)
            {
                return RedirectToAction("Index", "salas");
            }
            else
            {
                return RedirectToAction("Entrar", "usuarios");
            }

        }
        public ActionResult Index()
        {
            return View(db.usuariosala.ToList());
        }

        // GET: usuariosalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuariosala usuariosala = db.usuariosala.Find(id);
            if (usuariosala == null)
            {
                return HttpNotFound();
            }
            return View(usuariosala);
        }

        // GET: usuariosalas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuariosalas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSala,idUsuario,proprietario,acessopermitido")] usuariosala usuariosala)
        {
            if (ModelState.IsValid)
            {
                db.usuariosala.Add(usuariosala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuariosala);
        }

        // GET: usuariosalas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuariosala usuariosala = db.usuariosala.Find(id);
            if (usuariosala == null)
            {
                return HttpNotFound();
            }
            return View(usuariosala);
        }

        // POST: usuariosalas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSala,idUsuario,proprietario,acessopermitido")] usuariosala usuariosala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuariosala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuariosala);
        }

        // GET: usuariosalas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuariosala usuariosala = db.usuariosala.Find(id);
            if (usuariosala == null)
            {
                return HttpNotFound();
            }
            return View(usuariosala);
        }

        // POST: usuariosalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuariosala usuariosala = db.usuariosala.Find(id);
            db.usuariosala.Remove(usuariosala);
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
