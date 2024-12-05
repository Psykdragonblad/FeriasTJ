namespace FeriasTJ.Domain.Entities
{
    public class Ferias
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public DateTime PeriodoAquisitivoInicial { get; set; }
        public DateTime PeriodoAquisitivoFinal { get; set; }
        public List<Usufruto> Usufrutos { get; set; }

    }
}
