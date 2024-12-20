﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FeriasTJBase.Domain.Entities
{
    [Table("ferias")]
    public class Ferias
    {
        public int IdFerias { get; set; }
        public int Matricula { get; set; }
        public DateTime PeriodoAquisitivoInicial { get; set; }
        public DateTime PeriodoAquisitivoFinal { get; set; }
        public List<Usufruto> Usufrutos { get; set; } = new ();

    }
}
