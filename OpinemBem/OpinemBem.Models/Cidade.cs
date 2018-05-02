using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class Cidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Estado Estado { get; set; }
    }
}
