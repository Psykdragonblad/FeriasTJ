using FeriasTJBase.Application.Interface;
using FeriasTJBase.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FeriasTJBase.Infra.Repositories.Base
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        private readonly PgDbContext _pgContext;
        private readonly DbSet<T> _dbSet;
        public ReadOnlyRepository(PgDbContext pgDbContext) 
        { 
            _pgContext = pgDbContext;
            _dbSet = _pgContext.Set<T>();
        }
        public async Task<T> ObterPeloId(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObterTodaLista()
        {
           return await _dbSet.ToListAsync();
        }
    }
}
