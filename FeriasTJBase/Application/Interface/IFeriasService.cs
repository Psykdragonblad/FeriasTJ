using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Application.Interface
{
    public interface IFeriasService
    {
        Task<List<Ferias>> GetAllFerias();
        Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorIdAsync(int id);
    }
}
