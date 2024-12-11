using AutoMapper;
using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;

namespace FeriasTJBase.Application.Services
{
    public class FeriasSerivce(IFeriasRepository feriasRepository, IMapper mapper) : IFeriasService
    {
        private readonly IFeriasRepository _feriasRepository = feriasRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Ferias>> GetAllFeriasComUsufruto()
        {
            return await _feriasRepository.GetAllFerias();            
        }
        public async Task<IEnumerable<ConsultaPeriodoAquisitivoDto>> GetAllFerias()
        {
            var periodo = await _feriasRepository.ObterTodaLista();
            return _mapper.Map<IEnumerable<ConsultaPeriodoAquisitivoDto>>(periodo);
        }

        public async Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorIdAsync(int id)
        {
           var ferias = await _feriasRepository.ObterPeloId(id);
            return _mapper.Map<ConsultaPeriodoAquisitivoDto>(ferias);
        }
    }
}
