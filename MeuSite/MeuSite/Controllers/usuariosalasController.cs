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
        private bancofcEntities db = new bancofcEntities();

        // GET: usuariosalas
        public ActionResult obj()
        {
            return View(db.usuariosala12.ToList());
        }
        public int Definir(int id)
        {
            return id;
        }
        public ActionResult AdmSala(int id, int usu)
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
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.acesso = TempData["Acesso"];
            ViewBag.id = id;
            List<usuariosala12> relacionados = db.usuariosala12.ToList().FindAll(s => s.sala_idsala == id);
            Membros membros = new Membros();
            //membros.listaPartiipantes = relacionados.FindAll(s => s.acessopermitido == true);
            //membros.listaPendentes = relacionados.FindAll(s => s.acessopermitido == false);
            return View(relacionados);
        }
        public ActionResult AdmSolicitacao(int id, int sala, Boolean permi, int user)
        {
            List<usuariosala12> items = db.usuariosala12.ToList().FindAll(j => j.sala_idsala == sala);
            usuariosala12 item = items.Find(k => k.usuario_idusuario == id);
            if (permi == true)
            {
                item.acessopermitido = permi;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.usuariosala12.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("AdmSala", new { id = sala, usu = user });
        }
        public ActionResult Download(int id)
        {
            return RedirectToAction("Download", "salas", new { id = id });
        }

        public ActionResult DesVincular(int id, int idar)
        {
            return RedirectToAction("DesVincular", "salas", new { id = id, idar = idar });
        }
        public ActionResult Mensagem(string mensagem)
        {
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            ViewBag.id = TempData["IDSALA"];
            TempData["IDSALA"] = ViewBag.id;
            ViewBag.per = TempData["adm"];
            TempData["adm"] = ViewBag.per;

            chat_usuario_sala obj = new chat_usuario_sala();
            sala objsala = db.sala.ToList().Find(i => i.idsala == ViewBag.id);
            usuario objusuario = db.usuario.ToList().Find(s => s.idusuario == ViewBag.idusuario);

            obj.sala_idsala = objsala.idsala;
            obj.usuario_idusuario = objusuario.idusuario;
            obj.sala_temasala_idtemasala = objsala.temasala_idtemasala;

            obj.mensagem = mensagem;

            List<chat_usuario_sala> m = objsala.chat_usuario_sala.ToList();
            int numero = m.Capacity;

            obj.numero = numero;
            if (obj.mensagem != null)
            {
                if (obj.mensagem != "")
                {
                    db.chat_usuario_sala.Add(obj);
                    db.SaveChanges();
                }
            }
            
            return RedirectToAction("Salas", new { id = ViewBag.id, usu = ViewBag.idusuario, gerencia = ViewBag.per });
        }
        public ActionResult Salas(int id, int usu, Boolean gerencia)
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
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            ViewBag.acesso = TempData["Acesso"];
            ViewBag.idsala = id;
            TempData["IDSALA"] = id;
            sala escolha = db.sala.ToList().Find(s => s.idsala == id);
            //TarefaChat lista = new TarefaChat();
            //List<chat> listaCH = new List<chat>();
            //List<tarefa> listaTa = new List<tarefa>();
            //listaCH = db.chat.ToList().FindAll(item => item.idSala == id);
            //listaTa = db.tarefa.ToList().FindAll(item => item.idSala == id);

            //lista.listatarefas = listaTa;
            //lista.listachat = listaCH;
            //lista.idSala = id;
            ViewBag.id = id;
            ViewBag.usuario = usu;
            TempData["Acesso"] = ViewBag.acesso;
            TempData["adm"] = gerencia;
            ViewBag.privilegios = gerencia;
            //return View(lista);
            ViewBag.sala_destino = escolha.nome;

            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;
            return View(escolha);
        }
        public ActionResult PesquisaBiblioteca(int id)
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
            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;

            ViewBag.sala_destino = TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            ViewBag.usuario = TempData["USUARIO+USER"];
            ViewBag.privilegios = TempData["PRIVILEGIOS"];

            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;
            return RedirectToAction("Index", "arquivobibliotecas");
        }
        public ActionResult Materiais(int id)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            TempData["IDSALA"] = id;

            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;

            ViewBag.sala_destino = TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            ViewBag.usuario = TempData["USUARIO+USER"];
            ViewBag.privilegios = TempData["PRIVILEGIOS"];

            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;

            return RedirectToAction("Materiais", "salas");
        }
        public ActionResult Tarefas(int id)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            TempData["IDSALA"] = id;
            return RedirectToAction("Create", "tarefas");
        }
        public ActionResult Arquivos(int id , int tare)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            TempData["IDSALA"] = id;
            TempData["tarefa"] = tare;
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
            usuario coleta = db.usuario.ToList().Find(item => item.idusuario == ViewBag.idusuario);
            //if (coleta == null)
            //{
            //    return RedirectToAction("Index", "usuarios");
            //}
            //usuario pessoa = new usuario();
            //foreach (var item in coleta)
            //{
            //    pessoa = item;
            //}
            if (coleta.conexao == false)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            TempData["ID"] = coleta.idusuario;
            TempData["Email"] = coleta.email;
            TempData["Acesso"] = coleta.conexao;
            ViewBag.acesso = TempData["Acesso"];
            List<usuariosala12> lista = db.usuariosala12.ToList().FindAll(item => item.usuario_idusuario == ViewBag.idusuario);
            //List<sala> listacvin = new List<sala>();
            //ViewBag.tc = "";
            //foreach (var item in lista)
            //{
            //    sala novo = db.sala.ToList().Find(s => s.idsala == item.sala_idsala);
            //    if (novo != null)
            //    {
            //        listacvin.Add(novo);
            //    }

            //}
            //List<sala> listasAdm = new List<sala>();
            //List<sala> listasPar = new List<sala>();
            //foreach (var item in lista)
            //{
            //    if (item.proprietario == true)
            //    {
            //        sala novo = listacvin.Find(s => s.idsala == item.sala_idsala);
            //        if (novo != null)
            //        {
            //            listasAdm.Add(novo);
            //        }
            //    }
            //    else
            //    {
            //        if (item.acessopermitido == true)
            //        {
            //            sala novo = listacvin.Find(s => s.idsala == item.sala_idsala);
            //            if (novo != null)
            //            {
            //                listasPar.Add(novo);
            //            }
            //        }
            //    }
            //}
            //AdmSalas listasala = new AdmSalas();
            //listasala.listaAdm = listasAdm;
            //listasala.listaPar = listasPar;
            TempData["Acesso"] = ViewBag.acesso;
            return View(lista);
        }
        public ActionResult CriarSala()
        {
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            if (ViewBag.idusuario != null)
            {
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
            usuariosala12 novo = new usuariosala12();
            sala m = db.sala.ToList().Find(s => s.idsala == id);
            novo.sala_idsala = id;
            novo.usuario_idusuario = iduse;
            novo.sala_temasala_idtemasala = m.temasala_idtemasala;
            novo.acessopermitido = false;
            novo.proprietario = false;
            db.usuariosala12.Add(novo);
            db.SaveChanges();
            return RedirectToAction("Index", "salas");

        }
        public ActionResult DesParticipar(int id, int iduse)
        {
            List<usuariosala12> li = db.usuariosala12.ToList().FindAll(s => s.sala_idsala == id);
            usuariosala12 obj = li.Find(s => s.usuario_idusuario == iduse);
            db.usuariosala12.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index", "salas");

        }
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
            ViewBag.idsala = TempData["Pids"];
            ViewBag.idusuario = TempData["ID"];
            TempData["ID"] = ViewBag.idusuario;
            List<usuariosala12> lista = db.usuariosala12.ToList().FindAll(item => item.sala_idsala == ViewBag.idsala);
            usuariosala12 relacao = new usuariosala12();
            if (lista != null)
            {
                relacao = lista.Find(s => s.usuario_idusuario == ViewBag.idusuario);
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
            usuariosala12 usuariosala = db.usuariosala12.Find(id);
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
        public ActionResult Create([Bind(Include = "idSala,idUsuario,proprietario,acessopermitido")] usuariosala12 usuariosala)
        {
            if (ModelState.IsValid)
            {
                db.usuariosala12.Add(usuariosala);
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
            List<usuariosala12> relacoes = db.usuariosala12.ToList().FindAll(s => s.usuario_idusuario == id);
            if (relacoes != null)
            {
                usuariosala12 solicitacao = relacoes.Find(s => s.sala_idsala == sala);
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
        public ActionResult Edit([Bind(Include = "idSala,idUsuario,proprietario,acessopermitido")] usuariosala12 usuariosala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuariosala).State = System.Data.Entity.EntityState.Modified;
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
            usuariosala12 usuariosala = db.usuariosala12.Find(id);
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
            usuariosala12 usuariosala = db.usuariosala12.Find(id);
            db.usuariosala12.Remove(usuariosala);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Confi(int id, Boolean entrega, int idsa, int usu)
        {
            tarefa item = db.tarefa.ToList().Find(i => i.idtarefa == id);
            item.entregue = entrega;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //return entrega;
            return RedirectToAction("Salas", new { id = idsa, usu = usu, gerencia = true });
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
