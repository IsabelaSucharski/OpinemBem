using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    [Authorize]
    public class PerfilAdmController : Controller
    {
        public ActionResult Index()
        {
            var usuarioLogado = new UsuarioDAO().BuscarPorId(((Usuario)User).Id);
            return View(usuarioLogado);
        }
    }
}