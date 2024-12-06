using FeriasTJBase.Domain.Entities;

namespace FeriasTJBase.Domain.Interface
{
    public interface IFeriasRepository
    {
        Task SalvarFerias(Ferias ferias);
    }
}
