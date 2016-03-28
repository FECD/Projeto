using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeuSite.Models;

namespace MeuSite.Controllers
{
    public class HomeController : Controller
    {
        private bancofcEntities db = new bancofcEntities();
        public ActionResult Sair()
        {
            return RedirectToAction("Sair", "usuarios");
        }
        public ActionResult Index()
        {
            ViewBag.Email = TempData["Email"];
            TempData["Email"] = ViewBag.Email;
            if (TempData["Email"] == null)
            {
                return RedirectToAction("Entrar", "usuarios");
            }
            List<usuario> lista = db.usuario.ToList().FindAll(item => item.email == ViewBag.Email);
            if (lista == null)
            {
                return RedirectToAction("Index", "usuarios");
            }
            usuario pessoa = new usuario();
            foreach (var item in lista)
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
            return View();
        }
        public ActionResult Perfil()
        {
            return RedirectToAction("Perfil", "usuarios");
        }
        public ActionResult SuasSalas()
        {
            if (TempData["Email"] == null)
            {
                return RedirectToAction("Entrar");
            }
            usuario pessoa = db.usuario.ToList().Find(s => s.email == TempData["Email"].ToString());
            if (pessoa.conexao == false)
            {
                return RedirectToAction("Entrar");
            }
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email;
            TempData["Acesso"] = pessoa.conexao;
            return RedirectToAction("SuasSalas", "usuariosalas");
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Em Desenvolvimento";

            return View();
        }

       
        }
}
