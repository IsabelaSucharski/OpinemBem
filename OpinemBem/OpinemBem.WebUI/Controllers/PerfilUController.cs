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
    public class PerfilUController : Controller
    {
        public ActionResult Index()
        {
            var lst = new ProjetoDeLeiDAO().BuscarPorUsuario(((Usuario)User).Id);
            return View(lst);
        }
    }
}