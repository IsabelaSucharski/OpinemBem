using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinemBem.Models
{
    public class Cadastro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNasc { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public TIPO_USUARIO Tipo { get; set; }
        public SEXO Sexo { get; set; }
    }
}
