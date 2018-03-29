using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime? DataNasc { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }
        public Sexo Sexo { get; set; }
        public bool Administrador { get; set; }
        public string Foto { get; set; }
        public string CaminhoFoto { get; set; }
    }
}
