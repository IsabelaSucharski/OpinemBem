using OpinemBem.DataAccess;
using OpinemBem.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OpinemBem.WebUI.Controllers
{
    public class LoginAdmController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrar(Usuario obj)
        {
            var usuarioLogado = new UsuarioDAO().LogarAdm(obj);

            if (usuarioLogado == null)
            {
                return View("Index");
            }

            //armazenando usuário ligado no cache do browser
            var userData = new JavaScriptSerializer().Serialize(usuarioLogado);
            FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Email, userData, false);

            return RedirectToAction("ProjetoAdm", "Projetos");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}