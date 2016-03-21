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
        public int Definir(int id)
        {
            return id;
        }
        public ActionResult AdmSala(int id, int usu)
        {
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
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.acesso = TempData["Acesso"];
            ViewBag.id = id;
            List<usuariosala> relacionados = db.usuariosala.ToList().FindAll(s => s.idSala == id);
            Membros membros = new Membros();
            membros.listaPartiipantes = relacionados.FindAll(s => s.acessopermitido == true);
            membros.listaPendentes = relacionados.FindAll(s => s.acessopermitido == false);
            return View(membros);
        }
        public ActionResult Salas(int id, int usu, Boolean gerencia)
        {
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
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.acesso = TempData["Acesso"];
            TarefaChat lista = new TarefaChat();
            List<chat> listaCH = new List<chat>();
            List<tarefa> listaTa = new List<tarefa>();
            listaCH = db.chat.ToList().FindAll(item => item.idSala == id);
            listaTa = db.tarefa.ToList().FindAll(item => item.idSala == id);

            lista.listatarefas = listaTa;
            lista.listachat = listaCH;
            lista.idSala = id;
            ViewBag.id = id;
            ViewBag.usuario = usu;
            TempData["Acesso"] = ViewBag.acesso;
            ViewBag.privilegios = gerencia;
            return View(lista);
        }
        public ActionResult PesquisaBiblioteca(int id)
        {
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
            TempData["IDSALA"] = id;
            return RedirectToAction("Index", "arquivobibliotecas");
        }
        public ActionResult Materiais(int id)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            TempData["IDSALA"] = id;
            return RedirectToAction("Materiais", "salas");
        }
        public ActionResult Tarefas(int id)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            TempData["IDSALA"] = id;
            return RedirectToAction("Create", "tarefas");
        }
        public ActionResult Arquivos(int id)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            TempData["IDSALA"] = id;
            return RedirectToAction("Create", "arquivobibliotecas");
        }
        public ActionResult SuasSalas()
        {
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
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.acesso = TempData["Acesso"];
            List<usuariosala> lista = db.usuariosala.ToList().FindAll(item => item.idUsuario == ViewBag.idusuario);
            List<sala> listacvin = new List<sala>();
            ViewBag.tc = "";
            foreach (var item in lista)
            {
                sala novo = db.sala.ToList().Find(s => s.idsala == item.idSala);
                if (novo != null)
                {
                    listacvin.Add(novo);
                }
                
            }
            List<sala> listasAdm = new List<sala>();
            List<sala> listasPar = new List<sala>();
            foreach (var item in lista)
            {
                if (item.proprietario == true){
                    sala novo = listacvin.Find(s => s.idsala == item.idSala);
                    if (novo != null)
                    {
                        listasAdm.Add(novo);
                    }
                }
                else{
                    if (item.acessopermitido == true){
                        sala novo = listacvin.Find(s => s.idsala == item.idSala);
                        if (novo != null)
                        {
                            listasPar.Add(novo);
                        }
                    }
                }
            }
            AdmSalas listasala = new AdmSalas();
            listasala.listaAdm = listasAdm;
            listasala.listaPar = listasPar;
            TempData["Acesso"] = ViewBag.acesso;
            return View(listasala);
        }
        public ActionResult CriarSala()
        {
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            if (ViewBag.idusuario != null){
                return RedirectToAction("Create", "salas");
            }
            else
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            
        }
        public ActionResult Pesquisa()
        {
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
            return RedirectToAction("Index", "salas");

        }
        public ActionResult Participar(int id, int iduse)
        {
            usuariosala novo = new usuariosala();
            novo.idSala = id;
            novo.idUsuario = iduse;
            novo.acessopermitido = false;
            novo.proprietario = false;
            db.usuariosala.Add(novo);
            db.SaveChanges();
            return RedirectToAction("Index", "salas");

        }
        public ActionResult DesParticipar(int id, int iduse)
        {
            List<usuariosala> li = db.usuariosala.ToList().FindAll(s => s.idSala == id);
            usuariosala obj = li.Find(s => s.idUsuario == iduse);
            db.usuariosala.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", "salas");

        }
        public ActionResult Index()
        {
            ViewBag.idsala = TempData["Pids"];
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            List<usuariosala> lista = db.usuariosala.ToList().FindAll(item => item.idSala == ViewBag.idsala);
            usuariosala relacao = new usuariosala();
            if (lista != null)
            {
                relacao = lista.Find(s => s.idUsuario == ViewBag.idusuario);
                if (relacao == null)
                {
                    ViewBag.resposta = "Você não é um membro dessa sala";
                }
                else
                {
                    if (relacao.proprietario == true)
                    {
                        ViewBag.resposta = "Você é um Administrador dessa sala";
                    }
                    if (relacao.acessopermitido == true)
                    {
                        ViewBag.resposta = "Você é um membro dessa sala";
                    }
                    if (relacao.acessopermitido == false)
                    {
                        ViewBag.resposta = "Você solicitou participação nessa sala";
                    }

                        
                }
            }
            return View(relacao);
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
        public ActionResult Edit(int id, int sala)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<usuariosala> relacoes = db.usuariosala.ToList().FindAll(s => s.idUsuario == id);
            if (relacoes != null)
            {
                usuariosala solicitacao = relacoes.Find(s => s.idSala == sala);
                if (solicitacao != null)
                {
                    return View(solicitacao);
                }
                else
                {
                    return RedirectToAction("Entrar", "usuarios");
                }
            }
            else
            {
                return RedirectToAction("Entrar", "usuarios");
            }
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
