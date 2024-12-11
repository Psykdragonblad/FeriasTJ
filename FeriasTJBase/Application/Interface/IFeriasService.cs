using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Application.Interface
{
    public interface IFeriasService 
    {
        Task<IEnumerable<ConsultaPeriodoAquisitivoDto>> GetAllFerias();
        Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorIdAsync(int id);
        Task<IEnumerable<Ferias>> GetAllFeriasComUsufruto();
    }
}
