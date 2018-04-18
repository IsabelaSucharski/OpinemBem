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
    public class ProjetosController : Controller
    {
        public ActionResult AlterarProjetos()
        {
            return View();
        }

        public ActionResult CadProjetoAdm()
        {
            ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
            return View();
        }

        public ActionResult SalvarProjetoAdm(ProjetoDeLei obj)
        {
            obj.Usuario = new Usuario() { Id = 1 };
            new ProjetoDeLeiDAO().Inserir(obj);
            return RedirectToAction("ProjetoAdm", "Projetos");
        }

        public ActionResult CadProjetos()
        {
            ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
            return View();
        }

        public ActionResult SalvarProjetoU(ProjetoDeLei obj)
        {
            obj.Usuario = new Usuario() { Id = 1 };
            new ProjetoDeLeiDAO().Inserir(obj);
            return RedirectToAction("ProjetoU", "Projetos");
        }

        public ActionResult ProjetoAdm()
        {
            var lst = new ProjetoDeLeiDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult ProjetoU()
        {
            var lst = new ProjetoDeLeiDAO().BuscarTodos();
            return View(lst);
        }
    }
}