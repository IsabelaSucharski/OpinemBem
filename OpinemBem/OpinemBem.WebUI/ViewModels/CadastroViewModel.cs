using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpinemBem.WebUI
{
    public class CadastroViewModel
    {
        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'CPF' é obrigatório!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo 'E-mail' é obrigatório!")]
        public string Email { get; set; }

        public DateTime? DataNasc { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' é obrigatório!")]
        public string Senha { get; set; }

        public Sexo? Sexo { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "Campo 'UF' é obrigatório!")]
        public int EstadoId { get; set; }

        [Required(ErrorMessage = "Campo 'Cidade' é obrigatório!")]
        public int CidadeId { get; set; }
    }
}