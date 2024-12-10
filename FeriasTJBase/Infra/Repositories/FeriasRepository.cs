using FeriasTJBase.Application.Dtos.Ferias;
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

        public async Task<List<Ferias>> GetAllFeriasAsync()
        {
            return  _pgDbContext.Set<Ferias>().Include(f => f.Usufrutos).ToList();
            /*return  _pgDbContext.Set<Ferias>()
                     .Include(f => f.Usufrutos)
                     .ToList();*/
            //return ferias;
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
        }

        public async Task SalvarFerias(Ferias ferias)
        {
            await _pgDbContext.Ferias.AddAsync(ferias);
            await _pgDbContext.SaveChangesAsync();
        }
    }
}
