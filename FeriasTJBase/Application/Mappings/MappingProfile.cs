using AutoMapper;
using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FeriasTJBase.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ferias, ConsultaPeriodoAquisitivoDto>();
            CreateMap<ConsultaPeriodoAquisitivoDto, Ferias>();
        }
    }
}
