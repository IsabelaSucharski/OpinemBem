﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinemBem.Models
{
    public class PerfilU
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string NivelUsuario { get; set; }
        public string ProgressoUsuario { get; set; }
        public string ProjetosVotados { get; set; }
        public string VotosContra { get; set; }
        public string VotosFavor { get; set; }
    }
}
