using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath(string.Format("~/Uploads/{0:yyyyMMdd}", DateTime.Now))))
                    Directory.CreateDirectory(Server.MapPath(string.Format("~/Uploads/{0:yyyyMMdd}", DateTime.Now)));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath(string.Format("~/Uploads/{0:yyyyMMdd}", DateTime.Now)), Path.GetFileName(f.FileName));
                    FileInfo fi = new FileInfo(savedFileName);
                    f.SaveAs(savedFileName);

                    return Json(new
                    {
                        Nome = fi.Name,
                        Caminho = fi.FullName,
                        Extensao = fi.Extension,
                        Tamanho = fi.Length
                    });
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