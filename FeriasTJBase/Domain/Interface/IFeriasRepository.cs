using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Domain.Interface
{
    public interface IFeriasRepository
    {
        Task SalvarFerias(Ferias ferias);

        Task<List<Ferias>> GetAllFeriasAsync();

        Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorId(int id);
    }
}
