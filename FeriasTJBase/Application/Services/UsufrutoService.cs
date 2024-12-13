using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;

namespace FeriasTJBase.Application.Services
{
    public class UsufrutoService(IUsufrutoRepository repository) : IUsufrutoService
    {
        private readonly IUsufrutoRepository _repository = repository;
        
        public async Task<IEnumerable<Usufruto>> GetAllUsufruto()
        {
            var lista = await _repository.ObterTodaLista();
            return lista.Where(e => e.Status).ToList();
        }

        public async Task<Usufruto> GetUsufrutoPeloId(int id)
        {
           var usufruto =  await _repository.ObterPeloId(id);
           return usufruto != null && usufruto.Status ? usufruto : null;
        }
    }
}
