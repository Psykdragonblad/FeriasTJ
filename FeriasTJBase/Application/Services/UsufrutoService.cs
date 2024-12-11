using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;

namespace FeriasTJBase.Application.Services
{
    public class UsufrutoService(IUsufrutoRepository repository) : IUsufrutoService
    {
        private readonly IUsufrutoRepository _repository = repository;
        
        public Task<IEnumerable<Usufruto>> GetAllUsufruto()
        {
            return _repository.ObterTodaLista();
        }

        public Task<Usufruto> GetUsufrutoPeloId(int id)
        {
           return _repository.ObterPeloId(id);
        }
    }
}
