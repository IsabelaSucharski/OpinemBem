using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OpinemBem.Models
{
    public class ProjetoDeLei
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'Categoria' é obrigatório!")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Campo 'Descrição' é obrigatório!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo 'Vantagens' é obrigatório!")]
        public string Vantagens { get; set; }

        [Required(ErrorMessage = "Campo 'Desvantagens' é obrigatório!")]
        public string Desvantagens { get; set; }

        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Campo 'Tempo Disponível' é obrigatório!")]
        public int TempoDisponivel { get; set; }

        public bool Publicado { get; set; }

        public int VotosContra { get; set; }

        public int VotosAFavor { get; set; }

        public int TotalDeVotos { get; set; }
    }
}
