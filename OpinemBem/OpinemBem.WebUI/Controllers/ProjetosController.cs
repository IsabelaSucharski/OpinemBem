using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    [Authorize]
    public class ProjetosController : Controller
    {
        public ActionResult AlterarProjeto(int id)
        {
            var lei = new ProjetoDeLeiDAO().BuscarPorId(id);
            ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
            return View(lei);
        }

        public ActionResult AtualizarP(ProjetoDeLei obj)
        {
            new ProjetoDeLeiDAO().Atualizar(obj);
            return RedirectToAction("Index", "PerfilAdm");
        }

        public ActionResult CadProjetos()
        {
            ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
            return View();
        }

        public ActionResult SalvarProjetoU(ProjetoDeLei obj)
        {
            if (ModelState.IsValid)
            {
                obj.Usuario = new Usuario() { Id = ((Usuario)User).Id };
                new ProjetoDeLeiDAO().Inserir(obj);
                return RedirectToAction("ProjetoU", "Projetos");
            }
            return View("CadProjetos");
        }

        public ActionResult ProjetoAdm()
        {
            var lst = new ProjetoDeLeiDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult ProjetoU()
        {
            var lst = new ProjetoDeLeiDAO().BuscarPublicados();
            return View(lst);
        }

        public ActionResult Votar(int id, string voto)
        {
            var obj = new Voto()
            {
                DataVoto = DateTime.Now,
                Usuario = new Usuario() { Id = ((Usuario)User).Id },
                ProjetoDeLei = new ProjetoDeLei() { Id = id },
                Valor = voto
            };

            new VotoDAO().Inserir(obj);

            return RedirectToAction("ProjetoU", "Projetos");
        }

        public ActionResult Publicar(int id)
        {
            var obj = new ProjetoDeLei() { Id = id, Publicado = true };
            new ProjetoDeLeiDAO().Publicar(obj);
            return RedirectToAction("AceitarLeisAdm", "Leis");
        }

        public ActionResult ExcluirProjeto(int id)
        {
            var obj = new ProjetoDeLei() { Id = id };
            new ProjetoDeLeiDAO().Deletar(obj);
            return RedirectToAction("Index", "PerfilAdm");
        }

        public ActionResult Buscar(string campoTexto)
        {
            var lst = new ProjetoDeLeiDAO().BuscarPublicados().Where(p => p.Nome.Contains(campoTexto)).ToList();
            return View("ProjetoU", lst);
        }
    }
}