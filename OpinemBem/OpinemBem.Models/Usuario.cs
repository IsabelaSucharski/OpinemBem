using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Script.Serialization;

namespace OpinemBem.Models
{
    public class Usuario : ICustomPrincipal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'CPF' é obrigatório!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo 'E-mail' é obrigatório!")]
        public string Email { get; set; }

        public DateTime? DataNasc { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' é obrigatório!")]
        public string Senha { get; set; }

        public string ConfirmaSenha { get; set; }

        public Sexo? Sexo { get; set; }

        public bool Administrador { get; set; }

        public string Foto { get; set; }

        public Cidade Cidade { get; set; }

        public Estado Estado { get; set; }

        [ScriptIgnore]
        public List<ProjetoDeLei> Leis { get; set; }

        [ScriptIgnore]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Email, "Usuario");
            }
            set { }
        }

        public bool IsInRole(string role)
        {
            return (role == "Admin");
        }

        public Usuario()
        {
            this.Leis = new List<ProjetoDeLei>();
        }

        public Usuario(string myEmail)
        {
            Identity = new GenericIdentity(myEmail, "Usuario");
        }
    }
}
