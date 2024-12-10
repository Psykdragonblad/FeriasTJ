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

        public async Task<List<Ferias>> GetAllFerias()
        {
            return await _feriasRepository.GetAllFeriasAsync();            
        }

        public Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorIdAsync(int id)
        {
           return  _feriasRepository.GetPeriodoAquisitivoPorId(id);
        }
    }
}
