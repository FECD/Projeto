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
using System.IO;

namespace MeuSite.Controllers
{
    public class salasController : Controller
    {
        private bancofcEntities db = new bancofcEntities();

        // GET: salas
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
            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;

            ViewBag.sala_destino = TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            ViewBag.usuario = TempData["USUARIO+USER"];
            ViewBag.privilegios = TempData["PRIVILEGIOS"];

            TempData["SALA+DESTINO"] = ViewBag.sala_destino;
            TempData["USUARIO+USER"] = ViewBag.usuario;
            TempData["PRIVILEGIOS"] = ViewBag.privilegios;
            ViewBag.id = TempData["IDSALA"];
            TempData["IDSALA"] = ViewBag.id;
            salasbibliotecaEtarefas listas = new salasbibliotecaEtarefas();
            List<biblioteca> listata = db.biblioteca.ToList();
            sala k = db.sala.ToList().Find(d => d.idsala == ViewBag.id);
            List<biblioteca> l = k.biblioteca.ToList();

            //List<biblioteca> e = k.tarefa.ToList().FindAll(s => s.biblioteca.ToList() != null);
            //List<a> listatadi = new List<arquivotarefa>();
            //List<salabiblioteca> listaBiblio = db.salabiblioteca.ToList().FindAll(sal => sal.idSala == ViewBag.id);
            //List<tarefa> tarefasdasala = db.tarefa.ToList().FindAll(objetivos => objetivos.sala_idsala == ViewBag.id);
            //foreach (var t in tarefasdasala)
            //{
            //    listata.Add(listata.Find(elemento => elemento.idTarefa == t.idtarefa));
            //}
            //listas.listaSalasBiblioteca = listaBiblio;
            //listas.listaArquivoTarefa = listata;
            return View(k);
        }
        public ActionResult DesVincular(int id, int idar)
        {
            return RedirectToAction("DesVincular", "arquivobibliotecas", new { id = id, arid = idar });
        }
        public FileResult Download(int id)
        {
            int arquivoId = Convert.ToInt32(id);
            List<biblioteca> arquivos = db.biblioteca.ToList();
            string nomeArquivo = arquivos.Find(s => s.idbiblioteca == id).caminho.ToString();
            //select arquivo.conteudo).First();
            //string contentType = "application/pdf";
            string nome = arquivos.Find(s => s.idbiblioteca == id).nome.ToString();
            string ext = arquivos.Find(s => s.idbiblioteca == id).caminho;
            var con = 0;
            var valor = 0;
            foreach (var n in ext)
            {
                if (n.ToString() == ".")
                {
                    valor += 1;
                }
                if (valor != 0)
                {
                    valor += 1;
                }
                else
                {
                    con += 1;
                }
            }
            string completo = nome + ".";
            for (var j = 0; j < valor; j++)
            {
                completo += ext[con + j - 1].ToString();
            }
            //string p1 = ext[con - 1].ToString();
            //string p2 = ext[con - 2].ToString();
            //string p3 = ext[con - 3].ToString();
            //string completo = nome + "." + p3 + p2 + p1;
            return File(nomeArquivo, nome, completo);

        }
        public ActionResult vercadastro(int id)
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

            TempData["Pids"] = id;
            return RedirectToAction("Index", "usuariosalas");
        }
        public ActionResult Filtrar(int idtemasala, string Nome)
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
            List<sala> lista = new List<sala>();
            List<sala> listasFinal = new List<sala>();
            if (idtemasala != 0)
            {
                ViewBag.Tema = db.temasala.ToList().Find(item => item.idtemasala == idtemasala).nome;
                lista = db.sala.ToList().FindAll(item => item.temasala_idtemasala == idtemasala);
                ViewBag.resposta = "Salas com temas " + ViewBag.Tema.ToString();
            }
            else
            {
                lista = db.sala.ToList();
                ViewBag.resposta = "Salas com qualquer tema";
            }
            if (Nome != "")
            {
                listasFinal = lista.FindAll(item => item.nome == Nome);
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

        //// GET: salas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    sala sala = db.sala.Find(id);
        //    if (sala == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sala);
        //}

        //// GET: salas/Create
        public ActionResult Create(int? idtema)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            if (ViewBag.mensagem == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            ViewBag.TemaSala = db.temasala.ToList();
            return View();
        }

        // POST: salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsala,nome,descricao,temasala_idtemasala")] sala sala, int idtemasala)
        {
            ViewBag.mensagem = TempData["ID"];
            TempData["ID"] = ViewBag.mensagem;
            if (TempData["ID"] == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            else
            {
                usuario user = db.usuario.ToList().Find(s => s.idusuario == ViewBag.mensagem);
                if (user == null)
                {
                    return RedirectToAction("Entrar", "usuarios");
                }
                //foreach (var item in db.usuario.ToList())
                //{
                //    if (ViewBag.mensagem.Equals(item.idusuario))
                //    {
                //        pessoa.idUsuario = item.idusuario;
                //        espa.idUsuario = item.idusuario;
                //    }
                //}
                sala.temasala_idtemasala = idtemasala;
                if (ModelState.IsValid)
                {
                    if (sala.temasala_idtemasala != null)
                    {
                        db.sala.Add(sala);
                        db.SaveChanges();
                        sala nitem = new sala();
                        List<sala> listageral = db.sala.ToList().FindAll(ik => ik.temasala_idtemasala == idtemasala);
                        List<sala> listageralNome = listageral.FindAll(s => s.nome == sala.nome);
                        foreach (var item in listageralNome)
                        {
                            if (item == sala)
                            {
                                nitem = item;
                            }
                        }
                        if (nitem == null)
                        {
                            return RedirectToAction("Entrar", "usuarios");
                        }
                        usuariosala12 adm = new usuariosala12();
                        adm.sala_idsala = nitem.idsala;
                        adm.sala_temasala_idtemasala = nitem.temasala_idtemasala;
                        adm.usuario_idusuario = ViewBag.mensagem;
                        adm.usuario = user;
                        adm.sala = nitem;
                        adm.acessopermitido = true;
                        adm.proprietario = true;
                        //chat_usuario_sala chat = new chat_usuario_sala();
                        //chat.sala_idsala = item.idsala;
                        //chat.sala_temasala_idtemasala = item.idsala;
                        //chat.usuario_idusuario = user.idusuario;
                        //chat.numero = 1;
                        //chat.mensagem = "";

                        db.usuariosala12.Add(adm);
                        db.SaveChanges();
                        TempData["Email"] = user.email;
                        return RedirectToAction("Index", "Home");
                    }


                }


                //sala atu = new sala();
                //foreach (var item in db.sala.ToList())
                //{
                //    if (item == sala)
                //    {
                //        atu = item;
                //    }
                //}
                //ViewBag.mensagem = TempData["ID"];
                //TempData["ID"] = ViewBag.mensagem;
                //usuariosala12 pessoa = new usuariosala12();
                //pessoa.sala_idsala = atu.idsala;
                //chat_usuario_sala espa = new chat_usuario_sala();
                //espa.sala_idsala = atu.idsala;
                //if (TempData["ID"] == null)
                //{
                //    return RedirectToAction("Entrar", "usuarios");
                //}
                //else
                //{
                //    foreach (var item in db.usuario.ToList())
                //    {
                //        if (ViewBag.mensagem.Equals(item.idusuario))
                //        {
                //            pessoa.idUsuario = item.idusuario;
                //            espa.idUsuario = item.idusuario;
                //        }
                //    }
                //}
                //pessoa.proprietario = true;
                //pessoa.acessopermitido = true;
                //espa.numero = 1;
                //espa.mensagem = "!-";
                //db.chat.Add(espa);
                //db.usuariosala.Add(pessoa);
                //db.SaveChanges();
                //return RedirectToAction("Index", "chats");
            }

            return View(sala);
        }

        //// GET: salas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    sala sala = db.sala.Find(id);
        //    if (sala == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sala);
        //}

        //// POST: salas/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idsala,idTemaSala,Nome,Descricao")] sala sala)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sala).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(sala);
        //}

        //// GET: salas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    sala sala = db.sala.Find(id);
        //    if (sala == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sala);
        //}

        //// POST: salas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    sala sala = db.sala.Find(id);
        //    db.sala.Remove(sala);
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
