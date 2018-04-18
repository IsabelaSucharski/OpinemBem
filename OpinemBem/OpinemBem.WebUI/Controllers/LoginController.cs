using OpinemBem.DataAccess;
using OpinemBem.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OpinemBem.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrar(Usuario obj)
        {
            var usuarioLogado = new UsuarioDAO().Logar(obj);

            if (usuarioLogado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //armazenando usuário ligado no cache do browser
            var userData = new JavaScriptSerializer().Serialize(usuarioLogado);
            FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Email, userData, false);

            return RedirectToAction("ProjetoU", "Projetos");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}