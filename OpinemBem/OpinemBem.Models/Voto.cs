using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class Voto
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public ProjetoDeLei ProjetoDeLei { get; set; }
        public DateTime DataVoto { get; set; }
        public string Valor { get; set; }
    }
}
