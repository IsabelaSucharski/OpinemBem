using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class Categoria
    {
        [Required(ErrorMessage = "Campo 'Categoria' é obrigatório!")]
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
