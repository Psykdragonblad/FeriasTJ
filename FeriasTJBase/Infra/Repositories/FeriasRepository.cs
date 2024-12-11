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
            return _pgDbContext.Set<Ferias>().Include(f => f.Usufrutos).ToList();
        }

        /* public async Task<List<Ferias>> GetAllFeriasAsync()
         {          
            return  _pgDbContext.Set<Ferias>().Include(f => f.Usufrutos).ToList();
         }

         public async Task<ConsultaPeriodoAquisitivoDto> GetPeriodoAquisitivoPorId(int id)
         {
             var ferias =  _pgDbContext.Set<Ferias>().FirstOrDefault(e => e.IdFerias == id);

             if (ferias == null) { 
                 return new ConsultaPeriodoAquisitivoDto();
             }

             return new ConsultaPeriodoAquisitivoDto()
             {
                 IdFerias = ferias.IdFerias,
                 Matricula = ferias.Matricula,
                 PeriodoAquisitivoInicial = ferias.PeriodoAquisitivoInicial,
                 PeriodoAquisitivoFinal = ferias.PeriodoAquisitivoFinal
             };
         }*/

        public async Task SalvarFerias(Ferias ferias)
        {
            await _pgDbContext.Ferias.AddAsync(ferias);
            await _pgDbContext.SaveChangesAsync();
        }
    }
}
