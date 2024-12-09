using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Application.Interface
{
    public interface IFeriasService
    {
        Task<IEnumerable<Ferias>> GetAllFerias();
    }
}
