using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;

namespace FeriasTJBase.Application.Services
{
    public class FeriasSerivce : IFeriasService
    {
        private readonly IFeriasRepository _feriasRepository;

        public FeriasSerivce(IFeriasRepository feriasRepository)
        {
            _feriasRepository = feriasRepository;
        }

        public Task<IEnumerable<Ferias>> GetAllFeriasComUsufruto()
        {
            return _feriasRepository.GetAllFerias();            
        }
        public async Task<IEnumerable<ConsultaPeriodoAquisitivoDto>> GetAllFerias()
        {
            var periodo = _feriasRepository.ObterTodaLista().Result;
            var dto = periodo.Select(e => new ConsultaPeriodoAquisitivoDto
            {
                IdFerias = e.IdFerias,
                Matricula = e.Matricula,
                PeriodoAquisitivoInicial = e.PeriodoAquisitivoInicial,
                PeriodoAquisitivoFinal = e.PeriodoAquisitivoFinal
            });
            return dto;
        }

        public async Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorIdAsync(int id)
        {
           var ferias = _feriasRepository.ObterPeloId(id).Result;
            return new ConsultaPeriodoAquisitivoDto()
            {
                IdFerias = ferias.IdFerias,
                Matricula = ferias.Matricula,
                PeriodoAquisitivoInicial = ferias.PeriodoAquisitivoInicial,
                PeriodoAquisitivoFinal = ferias.PeriodoAquisitivoFinal
            };
        
        }
    }
}
