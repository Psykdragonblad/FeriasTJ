namespace FeriasTJBase.Application.Dtos.Ferias
{
    public class ConsultaPeriodoAquisitivoDto
    {
        public int IdFerias { get; set; }
        public int Matricula { get; set; }
        public DateTime PeriodoAquisitivoInicial { get; set; }
        public DateTime PeriodoAquisitivoFinal { get; set; }
    }
}
