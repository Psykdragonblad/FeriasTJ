using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;
using FeriasTJBase.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FeriasTJBase.Infra.Repositories
{
    public class FeriasRepository : IFeriasRepository
    {
        private readonly PgDbContext _pgDbContext;

        public FeriasRepository(PgDbContext pgDbContext)
        {
            _pgDbContext = pgDbContext;
        }

        public async Task<IEnumerable<Ferias>> GetAllFeriasAsync()
        {
            return await _pgDbContext.Set<Ferias>().ToListAsync();          
        }

        public async Task SalvarFerias(Ferias ferias)
        {
            await _pgDbContext.Ferias.AddAsync(ferias);
            await _pgDbContext.SaveChangesAsync();
        }
    }
}
