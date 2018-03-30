using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class ProjetoDeLei
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public string Descricao { get; set; }
        public string Vantagens { get; set; }
        public string Desvantagens { get; set; }
        public Usuario Usuario { get; set; }
    }
}
