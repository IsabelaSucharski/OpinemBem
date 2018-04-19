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
            var lst = new CategoriaDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarCategoria(Categoria obj)
        {
            new CategoriaDAO().Inserir(obj);

            return RedirectToAction("Index", "Categoria");
        }

        public ActionResult Cadastro()
        {
            return View();
        }
    }
}