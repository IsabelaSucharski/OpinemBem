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

        public ActionResult Entrar(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    CPF = obj.CPF,
                    Senha = obj.Senha
                };

                if (!ValidarCPF(obj.CPF))
                {
                    ViewBag.MsgErro("CPF inválido!");
                    return View("Index");
                }

                var usuarioLogado = new UsuarioDAO().LogarAdm(usuario);

                if (usuarioLogado == null)
                {
                    return View("Index");
                }

                //armazenando usuário logado no cache do browser
                var userData = new JavaScriptSerializer().Serialize(usuarioLogado);
                FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Email, userData, false);

                return RedirectToAction("Index", "PerfilAdm");
            }

            return View("Index");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "LoginAdm");
        }

        private bool ValidarCPF(string cpf)
        {
            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }
}