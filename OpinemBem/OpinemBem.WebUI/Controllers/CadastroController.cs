using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class CadastroController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Estados = new EstadoDAO().BuscarTodos();
            ViewBag.Cidades = new List<Cidade>();
            return View();
        }

        public ActionResult SalvarUsuario(CadastroViewModel obj)
        {
            //valida os campos relacionados à classe
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    Nome = obj.Nome,
                    DataNasc = obj.DataNasc,
                    CPF = obj.CPF,
                    Email = obj.Email,
                    Sexo = obj.Sexo,
                    Senha = obj.Senha,
                    Estado = new Estado() { Id = obj.EstadoId },
                    Cidade = new Cidade() { Id = obj.CidadeId }
                };

                new UsuarioDAO().Inserir(usuario);

                return RedirectToAction("Index", "Login");
            }

            return View("Index");
        }

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("~/Uploads")))
                    Directory.CreateDirectory(Server.MapPath("~/Uploads"));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(f.FileName));
                    FileInfo fi = new FileInfo(savedFileName);
                    f.SaveAs(savedFileName);

                    return Json(fi.Name);
                }
                return Json(null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetCidades(int uf)
        {
            try
            {
                var lst = new CidadeDAO().BuscarPorUF(uf);
                return Json(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}