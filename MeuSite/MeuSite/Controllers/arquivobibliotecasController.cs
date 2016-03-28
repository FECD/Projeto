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
        private bancofcEntities db = new bancofcEntities();
        // GET: arquivobibliotecas
        public ActionResult Index()
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
        public ActionResult Filtrar(string Nome)
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
            List<biblioteca> publica = db.biblioteca.ToList().FindAll(pu => pu.privacidade == true);
            sala kj = db.sala.ToList().Find(k => k.idsala == ViewBag.idsala);
            List<biblioteca> lista = new List<biblioteca>();
            List<biblioteca> listasFinal = new List<biblioteca>();
            ViewBag.Tema = kj.temasala.nome;
            lista = publica.FindAll(item => item.temasala_idtemasala== kj.temasala_idtemasala);
            ViewBag.Mensagem = "Arquivos com temas " + ViewBag.Tema.ToString();
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
            ViewBag.Nome = Nome;

            return View(listasFinal);
            //List<sala> listinha = sala.FindAll(s => s.Nome =="IF");
            //return RedirectToAction("Index","salas");
        }
        public ActionResult ArquivoCond(int id, int idsala)
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
            sala k = db.sala.ToList().Find(s => s.idsala == ViewBag.idsala);
            biblioteca relacao = k.biblioteca.ToList().Find(s => s.idbiblioteca == ViewBag.Arid);
            //biblioteca relacao = o.Find(s => s.i == ViewBag.idsala);
            

            //List<salabiblioteca> lista = db.biblioteca.ToList().FindAll(s => s.idbiblioteca == ViewBag.Arid).;
            //salabiblioteca relacao = new salabiblioteca();
            //if (lista != null)
            //{
            //    relacao = lista.Find(item => item.idSala == ViewBag.idsala);
            if (relacao == null)
            {
                ViewBag.resposta = "Esse arquivo não é vinculado a sua sala";
            }
            else
            {
                ViewBag.resposta = "Esse arquivo é vinculado a sua sala";
            }
            //}
            return View(relacao);
        }
        public ActionResult Vincular(int id, int arid)
        {
            biblioteca novo = db.biblioteca.ToList().Find(d => d.idbiblioteca == arid);
            sala objsala = db.sala.ToList().Find(s => s.idsala == id);
            novo.temasala_idtemasala = objsala.temasala_idtemasala;
            novo.sala.Add(objsala);
            db.Entry(novo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //int id, int usu, Boolean gerencia
            return RedirectToAction("Materiais", "salas");
        }
        public ActionResult DesVincular(int id, int arid)
        {
            //List<biblioteca> li = db.salabiblioteca.ToList().FindAll(s => s.idSala == id);
            biblioteca obj = db.biblioteca.ToList().Find(s => s.idbiblioteca == arid);
            sala objsala = db.sala.ToList().Find(s => s.idsala == id);
            obj.sala.Remove(objsala);
            //db.salabiblioteca.Remove(obj);
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Materiais", "salas");

        }





        //// GET: arquivobibliotecas/Details/5
        
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
        //    if (arquivobiblioteca == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(arquivobiblioteca);
        //}

        //// GET: arquivobibliotecas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: arquivobibliotecas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idarquivoBiblioteca,idTemaSala,nome,conteudo,privacidade")] biblioteca arquivobiblioteca, HttpPostedFileBase file)
        {
            ViewBag.id = TempData["IDSALA"];
            ViewBag.idTarefa = TempData["tarefa"];
            sala sala = db.sala.ToList().Find(item => item.idsala == ViewBag.id);
            tarefa ta = db.tarefa.ToList().Find(l => l.idtarefa == ViewBag.idTarefa);
            TempData["IDSALA"] = ViewBag.id;
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName.ToString());
                var path = Path.Combine(Server.MapPath("~/Arquivos/Biblioteca"), fileName);
                file.SaveAs(path);
                path = Url.Content(Path.Combine("~/Arquivos/Biblioteca", fileName));
                arquivobiblioteca.caminho = path;
                arquivobiblioteca.temasala_idtemasala = sala.temasala_idtemasala;
                arquivobiblioteca.nome = file.FileName.ToString();
                arquivobiblioteca.tarefa.Add(ta);
                db.biblioteca.Add(arquivobiblioteca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arquivobiblioteca);
        }

        //// GET: arquivobibliotecas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
        //    if (arquivobiblioteca == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(arquivobiblioteca);
        //}

        //// POST: arquivobibliotecas/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idarquivoBiblioteca,idTemaSala,nome,conteudo")] biblioteca arquivobiblioteca)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(arquivobiblioteca).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(arquivobiblioteca);
        //}

        //// GET: arquivobibliotecas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
        //    if (arquivobiblioteca == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(arquivobiblioteca);
        //}

        //// POST: arquivobibliotecas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    arquivobiblioteca arquivobiblioteca = db.arquivobiblioteca.Find(id);
        //    db.arquivobiblioteca.Remove(arquivobiblioteca);
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
