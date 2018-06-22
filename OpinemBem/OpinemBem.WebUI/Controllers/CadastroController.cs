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
                if (!ValidarCPF(obj.CPF))
                {
                    ViewBag.Estados = new EstadoDAO().BuscarTodos();
                    ViewBag.Cidades = new List<Cidade>();
                    ViewBag.MsgErro = "CPF inválido!";
                    return View("Index");
                }

                if (!ValidarEmail(obj.Email))
                {
                    ViewBag.Estados = new EstadoDAO().BuscarTodos();
                    ViewBag.Cidades = new List<Cidade>();
                    ViewBag.MsgErro = "E-mail inválido!";
                    return View("Index");
                }

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

        private bool ValidarEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;
            if (!email.Contains("@") || !email.Contains("."))
                return false;
            string[] strCamposEmail = email.Split(new String[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length != 2)
                return false;
            if (strCamposEmail[0].Length < 3)
                return false;
            if (!strCamposEmail[1].Contains("."))
                return false;
            strCamposEmail = strCamposEmail[1].Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (strCamposEmail.Length < 2)
                return false;
            if (strCamposEmail[0].Length < 1)
                return false;
            return true;
        }
    }
}