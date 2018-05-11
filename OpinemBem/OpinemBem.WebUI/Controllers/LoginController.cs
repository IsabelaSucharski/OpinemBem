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

        public ActionResult Entrar(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    CPF = obj.CPF,
                    Senha = obj.Senha
                };

                var usuarioLogado = new UsuarioDAO().Logar(usuario);

                if (usuarioLogado == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                //armazenando usuário logado no cache do browser
                var userData = new JavaScriptSerializer().Serialize(usuarioLogado);
                FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Email, userData, false);

                return RedirectToAction("ProjetoU", "Projetos");
            }

            return View("Index");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }

        //public ActionResult EsquecerSenha(EsqSenhaViewModel obj)
        //{          
        //        var usuario = new Usuario()
        //        {
        //            Email = obj.Email,
        //            Senha = obj.Senha
        //        };
        //    return RedirectToAction("LoginU", "Login");
        //}

        public ActionResult EsqSenha()
        {
            return View();
        }
    }
}