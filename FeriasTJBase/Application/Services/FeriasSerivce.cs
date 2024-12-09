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

        public Task<IEnumerable<Ferias>> GetAllFerias()
        {
            return _feriasRepository.GetAllFeriasAsync();
        }
    }
}
