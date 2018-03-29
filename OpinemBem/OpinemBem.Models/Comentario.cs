using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public ProjetoDeLei ProjetoDeLei { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
    }
}
