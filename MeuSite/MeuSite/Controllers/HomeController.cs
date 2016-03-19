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
        edbancoEntities db = new edbancoEntities();
        public ActionResult Sair()
        {
            TempData.Clear();
            return RedirectToAction("Sair", "usuarios");
        }
        public ActionResult Index()
        {
            if (TempData["Email"] == null)
            {
                return RedirectToAction("Entrar", "usuarios");
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
                return RedirectToAction("Entrar", "usuarios");
            }
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
                return RedirectToAction("Entrar", "usuarios");
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
                return RedirectToAction("Entrar", "usuarios");
            }
            TempData["ID"] = pessoa.idusuario;
            TempData["Email"] = pessoa.email; 
            return RedirectToAction("SuasSalas", "usuariosalas");
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Em Desenvolvimento";

            return View();
        }

       
        }
}
