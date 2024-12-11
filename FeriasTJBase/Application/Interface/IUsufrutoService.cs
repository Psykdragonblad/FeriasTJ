using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Application.Interface
{
    public interface IUsufrutoService
    {
        Task<Usufruto> GetUsufrutoPeloId(int id);
        Task<IEnumerable<Usufruto>> GetAllUsufruto();
    }
}
