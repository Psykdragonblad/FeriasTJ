using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;
using FeriasTJBase.Infra.Configurations;
using FeriasTJBase.Infra.Repositories.Base;

namespace FeriasTJBase.Infra.Repositories
{
    public class UsufrutoRepository : ReadOnlyRepository<Usufruto>, IUsufrutoRepository
    {
        public UsufrutoRepository(PgDbContext pgDbContext) : base(pgDbContext)
        {
        }
    }
}
