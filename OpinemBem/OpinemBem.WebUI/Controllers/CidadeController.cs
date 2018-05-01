using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalvarCidade(Cidade obj)
        {
            new CidadeDAO().Inserir(obj);
            //inserir o obj na lista de categoria
            return RedirectToAction("Index", "Estado");
        }
    }
}