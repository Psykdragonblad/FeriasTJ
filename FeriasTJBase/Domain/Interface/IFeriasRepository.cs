using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Domain.Interface
{
    public interface IFeriasRepository : IReadOnlyRepository<Ferias>
    {
        Task SalvarFerias(Ferias ferias);

        Task<IEnumerable<Ferias>> GetAllFerias();

        //Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorId(int id);
    }
}
