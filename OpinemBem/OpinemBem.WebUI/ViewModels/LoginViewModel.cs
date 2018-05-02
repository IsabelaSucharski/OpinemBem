using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpinemBem.WebUI
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo 'CPF' é obrigatório!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' é obrigatório!")]
        public string Senha { get; set; }
    }
}