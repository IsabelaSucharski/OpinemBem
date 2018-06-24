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
            //vai buscar e aparecer todos na categ lst
            var lst = new EstadoDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarEstado(Estado obj)
        {
            new EstadoDAO().Inserir(obj);
            //inserir o obj na lista de categoria
            return RedirectToAction("Index", "Estado");
        }

        public ActionResult ExcluirEstado(int id)
        {
            var obj = new Estado() { Id = id };
            new EstadoDAO().Deletar(obj);
            return RedirectToAction("Index", "Estado");
        }

        public ActionResult CadastroE()
        {
            return View();
        }
    }
}