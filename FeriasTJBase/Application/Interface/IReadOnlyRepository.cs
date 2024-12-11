namespace FeriasTJBase.Application.Interface
{
    public interface IReadOnlyRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodaLista();
        Task<T> ObterPeloId(int id);
    }
}
