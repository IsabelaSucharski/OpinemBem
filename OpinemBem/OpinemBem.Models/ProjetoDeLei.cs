using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

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

        [Required(ErrorMessage = "Campo 'Tempo Disponível' é obrigatório!")]
        public int TempoDisponivel { get; set; }

        public Usuario Usuario { get; set; }

        public bool Publicado { get; set; }

        [ScriptIgnore]
        public int VotosContra { get; set; }

        [ScriptIgnore]
        public int VotosAFavor { get; set; }

        [ScriptIgnore]
        public int TotalDeVotos { get; set; }

        [ScriptIgnore]
        public Voto Voto { get; set; }

        [ScriptIgnore]
        public List<Comentario> Comentarios { get; set; }

        [ScriptIgnore]
        public decimal PercAFavor
        {
            get
            {
                if (!(this.TotalDeVotos > decimal.Zero))
                    return decimal.Zero;
                return (this.VotosAFavor * 100m) / (this.TotalDeVotos);
            }
        }

        [ScriptIgnore]
        public decimal PercContra
        {
            get
            {
                if (!(this.TotalDeVotos > decimal.Zero))
                    return decimal.Zero;
                return (this.VotosContra * 100m) / (this.TotalDeVotos);
            }
        }

        public ProjetoDeLei()
        {
            this.Comentarios = new List<Comentario>();
        }
    }
}
