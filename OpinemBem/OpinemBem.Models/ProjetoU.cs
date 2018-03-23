using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinemBem.Models
{
    public class ProjetoU
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TempoDispo { get; set; }
        public TIPO_USUARIO Tipo { get; set; }
        public SEXO Sexo { get; set; }
    }
}
