using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpinemBem.WebUI
{
    public class EsqSenhaViewModel
    {

        [Required(ErrorMessage = "Campo 'Email' é obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' é obrigatório!")]
        public string Senha { get; set; }

        public string ConfirmaSenha { get; set; }
    }
}