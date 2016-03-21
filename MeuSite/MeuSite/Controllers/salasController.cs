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
    public class salasController : Controller
    {
        private edbancoEntities db = new edbancoEntities();
        

        // GET: salas
        public ActionResult Index(){
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            if (ViewBag.idusuario == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            List<usuario> coleta = db.usuario.ToList().FindAll(item => item.idusuario == ViewBag.idusuario);
            if (coleta == null)
            {
                return RedirectToAction("Index", "usuarios");
            }
            usuario pessoa = new usuario();
            foreach (var item in coleta)
            {
                pessoa = item;
            }
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.Id = pessoa.idusuario;
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            //TempData["ID"] = ViewBag.idusuario;
            //ViewBag.idusuario = TempData["ID"];
            ViewBag.mensagem = ViewBag.idusuario;

            List<temasala> listas = db.temasala.ToList();
        //{
        //    TemasESalas listas = new TemasESalas();
        //    listas.listaTemas = db.temasala.ToList();
        //    listas.listaSalas = db.sala.ToList();
            return View(listas);
        }
        public ActionResult Materiais()
        {
            ViewBag.id = TempData["IDSALA"];
            TempData["IDSALA"] = ViewBag.id;
            salasbibliotecaEtarefas listas = new salasbibliotecaEtarefas();
            List<arquivotarefa> listata = db.arquivotarefa.ToList();
            List<arquivotarefa> listatadi = new List<arquivotarefa>();
            List<salabiblioteca> listaBiblio = db.salabiblioteca.ToList().FindAll(sal => sal.idSala == ViewBag.id);
            List<tarefa> tarefasdasala = db.tarefa.ToList().FindAll(objetivos => objetivos.idSala == ViewBag.id);
            //foreach (var t in tarefasdasala)
            //{
            //    listata.Add(listata.Find(elemento => elemento.idTarefa == t.idtarefa));
            //}
            listas.listaSalasBiblioteca = listaBiblio;
            listas.listaArquivoTarefa = listata;
            return View(listas);
        }
        public ActionResult vercadastro(int id)
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
            
            TempData["Pids"] = id;
            return RedirectToAction("Index", "usuariosalas");
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
            //usuario pessoa = new usuario();
            //foreach (var item in coleta)
            //{
            //    pessoa = item;
            //}
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.Id = pessoa.idusuario;
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            List<sala> lista = new List<sala>();
            List<sala> listasFinal = new List<sala>();
            if (idtemasala != 0) {
                ViewBag.Tema = db.temasala.ToList().Find(item => item.idTemaSala == idtemasala).nome;
                lista = db.sala.ToList().FindAll(item => item.idTemaSala == idtemasala);
                ViewBag.resposta = "Salas com temas " + ViewBag.Tema.ToString();
            }
            else
            {
                lista = db.sala.ToList();
                ViewBag.resposta = "Salas com qualquer tema";
            }
            if (Nome != "")
            {
                listasFinal = lista.FindAll(item => item.Nome == Nome);
                ViewBag.resposta += " e com o nome " + Nome;
            }
            else
            {
                listasFinal = lista;
                ViewBag.resposta += " E qualquer nome";
            }
            ViewBag.Tema = idtemasala;
            ViewBag.Nome = Nome;
            
            return View(listasFinal);
            //List<sala> listinha = sala.FindAll(s => s.Nome =="IF");
            //return RedirectToAction("Index","salas");
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
        public ActionResult Create([Bind(Include = "idsala,idTemaSala,Nome,Descricao")] sala sala)
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
        public ActionResult Edit([Bind(Include = "idsala,idTemaSala,Nome,Descricao")] sala sala)
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
