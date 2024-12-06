using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;
using FeriasTJBase.Infra.Configurations;

namespace FeriasTJBase.Infra.Repositories
{
    public class FeriasRepository : IFeriasRepository
    {
        private readonly PgDbContext _pgDbContext;

        public FeriasRepository(PgDbContext pgDbContext)
        {
            _pgDbContext = pgDbContext;
        }

        public async Task SalvarFerias(Ferias ferias)
        {
            await _pgDbContext.Ferias.AddAsync(ferias);
            await _pgDbContext.SaveChangesAsync();
        }
    }
}
