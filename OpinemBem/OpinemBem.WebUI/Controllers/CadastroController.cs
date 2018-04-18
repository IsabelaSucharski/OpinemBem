using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    public class CadastroController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalvarUsuario(Usuario obj)
        {
            if (ModelState.IsValid)
            {
                new UsuarioDAO().Inserir(obj);
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
    }
}