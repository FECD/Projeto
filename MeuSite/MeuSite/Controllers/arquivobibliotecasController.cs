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
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            if (ViewBag.idusuario == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            usuario pessoa = db.usuario.ToList().Find(item => item.idusuario == ViewBag.idusuario);
            if (pessoa == null)
            {
                return RedirectToAction("Index", "usuarios");
            }
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.Id = pessoa.idusuario;
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.idsala = TempData["IDSALA"];
            TempData["IDSALA"] = ViewBag.idsala;
            return View(db.temasala.ToList());
        }
        public ActionResult Filtrar(int idtemasala, string Nome)
        {
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            if (ViewBag.idusuario == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            usuario pessoa = db.usuario.ToList().Find(item => item.idusuario == ViewBag.idusuario);
            if (pessoa == null)
            {
                return RedirectToAction("Index", "usuarios");
            }
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.Id = pessoa.idusuario;
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.idsala = TempData["IDSALA"];
            TempData["IDSALA"] = ViewBag.idsala;
            List<arquivobiblioteca> lista = new List<arquivobiblioteca>();
            List<arquivobiblioteca> listasFinal = new List<arquivobiblioteca>();
            if (idtemasala != 0)
            {
                ViewBag.Tema = db.temasala.ToList().Find(item => item.idTemaSala == idtemasala).nome;
                lista = db.arquivobiblioteca.ToList().FindAll(item => item.idTemaSala == idtemasala);
                ViewBag.Mensagem = "Arquivos com temas " + ViewBag.Tema.ToString();
            }
            else
            {
                lista = db.arquivobiblioteca.ToList();
                ViewBag.Mensagem = "Arquivos com qualquer tema";
            }
            if (Nome != "")
            {
                listasFinal = lista.FindAll(item => item.nome == Nome);
                ViewBag.Mensagem += " e com o nome " + Nome;
            }
            else
            {
                listasFinal = lista;
                ViewBag.Mensagem += " E qualquer nome";
            }
            ViewBag.Tema = idtemasala;
            ViewBag.Nome = Nome;

            return View(listasFinal);
            //List<sala> listinha = sala.FindAll(s => s.Nome =="IF");
            //return RedirectToAction("Index","salas");
        }
        public ActionResult ArquivoCond(int id, int idsala)
        {
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            if (ViewBag.idusuario == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            usuario pessoa = db.usuario.ToList().Find(item => item.idusuario == ViewBag.idusuario);
            if (pessoa == null)
            {
                return RedirectToAction("Index", "usuarios");
            }
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.Id = pessoa.idusuario;
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.Arid = id;
            ViewBag.idsala = idsala;
            List<salabiblioteca> lista = db.salabiblioteca.ToList().FindAll(s => s.idArquivobiblioteca == ViewBag.Arid);
            salabiblioteca relacao = new salabiblioteca();
            if (lista != null)
            {
                relacao = lista.Find(item => item.idSala == ViewBag.idsala);
                if (relacao == null)
                {
                    ViewBag.resposta = "Esse arquivo não é vinculado a sua sala";
                }
                else
                {
                    ViewBag.resposta = "Esse arquivo é vinculado a sua sala";
                }
            }
            return View(relacao);
        }
        public ActionResult Vincular(int id,int arid )
        {
            salabiblioteca novo = new salabiblioteca();
            novo.idSala = id;
            novo.idArquivobiblioteca = arid;
            db.salabiblioteca.Add(novo);
            db.SaveChanges();
            return RedirectToAction("Index", "arquivobibliotecas");
        }
        public ActionResult DesVincular(int id, int arid)
        {
            List<salabiblioteca> li = db.salabiblioteca.ToList().FindAll(s => s.idSala == id);
            salabiblioteca obj = li.Find(s => s.idArquivobiblioteca == arid);
            db.salabiblioteca.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", "arquivobibliotecas");

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
                arquivobiblioteca.nome = fileName.ToString();
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
