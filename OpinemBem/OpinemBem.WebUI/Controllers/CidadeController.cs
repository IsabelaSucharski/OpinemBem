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
            //vai buscar e aparecer todos na categ lst
            var lst = new CidadeDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarCidade(Cidade obj)
        {
            new CidadeDAO().Inserir(obj);
            //inserir o obj na lista de categoria
            return RedirectToAction("Index", "Cidade");
        }

        public ActionResult CadastroC()
        {
            ViewBag.Estados = new EstadoDAO().BuscarTodos();
            return View();
        }
    }
}