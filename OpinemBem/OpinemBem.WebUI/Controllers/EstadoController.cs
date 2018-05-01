using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalvarEstado(Estado obj)
        {
            new EstadoDAO().Inserir(obj);
            //inserir o obj na lista de categoria
            return RedirectToAction("Index", "Estado");
        }
    }
}