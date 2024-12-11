using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;
using FeriasTJBase.Infra.Configurations;
using FeriasTJBase.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FeriasTJBase.Infra.Repositories
{
    public class FeriasRepository : ReadOnlyRepository<Ferias>, IFeriasRepository
    {
        private readonly PgDbContext _pgDbContext;

        public FeriasRepository(PgDbContext pgDbContext) : base(pgDbContext)
        {
            _pgDbContext = pgDbContext;
        }

        public async Task<IEnumerable<Ferias>> GetAllFerias()
        {
            return await _pgDbContext.Set<Ferias>().AsNoTracking().Include(f => f.Usufrutos).ToListAsync();               
        }

        public async Task SalvarFerias(Ferias ferias)
        {
            await _pgDbContext.Ferias.AddAsync(ferias);
            await _pgDbContext.SaveChangesAsync();
        }
    }
}
