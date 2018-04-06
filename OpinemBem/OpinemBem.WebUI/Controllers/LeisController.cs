using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class LeisController : Controller
    {
        public ActionResult AceitarLeisAdm()
        {
            var lst = new ProjetoDeLeiDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult ConcordaLeisAdm()
        {
            return View();
        }

        public ActionResult ConcordaLeisU()
        {
            return View();
        }

        public ActionResult GerenciarLeisAdm()
        {
            return View();
        }

        public ActionResult SalvarLeisAdm(ProjetoDeLei obj)
        {
            new ProjetoDeLeiDAO().Inserir(obj);
            return RedirectToAction("AceitasLeisAdm", "Leis");
        }
    }
}