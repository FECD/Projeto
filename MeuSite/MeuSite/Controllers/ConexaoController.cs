using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeuSite.Models;

namespace MeuSite.Controllers
{
    public class ConexaoController : Controller
    {
        // GET: Conexao
        public ActionResult Sair()
        {
            return RedirectToAction("Entrar", "usuarios");
        }
    }
}