using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class CategoriaController : Controller
    {
        public ActionResult Index()
        {
            //vai buscar e aparecer todos na categ lst
            var lst = new CategoriaDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarCategoria(Categoria obj)
        {
            new CategoriaDAO().Inserir(obj);
            //inserir o obj na lista de categoria
            return RedirectToAction("Index", "Categoria");
        }

        public ActionResult Cadastro()
        {
            return View();
        }
    }
}