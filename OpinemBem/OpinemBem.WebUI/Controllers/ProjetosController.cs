using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class ProjetosController : Controller
    {
        public ActionResult AlterarProjetos()
        {
            return View();
        }

        public ActionResult CadProjetoAdm()
        {
            return View();
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
            return View();
        }

        public ActionResult ProjetoU()
        {
            return View();
        }
    }
}