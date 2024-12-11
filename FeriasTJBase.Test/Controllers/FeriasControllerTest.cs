using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FeriasTJBase.Test.Controllers
{
    public class FeriasControllerTest
    {
        private readonly Mock<IFeriasService> _feriasServiceMoq;
        private readonly FeriasController _feriasController;
        

        public FeriasControllerTest()
        {
            _feriasServiceMoq = new Mock<IFeriasService>();
            _feriasController = new FeriasController(_feriasServiceMoq.Object);
        }

        [Fact]
        public async void RetornaListaDeFerias()
        {
            var ferias = new Ferias
            {
                IdFerias = 1,
                Matricula = 5007950,
                PeriodoAquisitivoInicial = Convert.ToDateTime("10/10/2020"),
                PeriodoAquisitivoFinal = Convert.ToDateTime("10/10/2021"),
                Usufrutos =
                {
                   new()
                   {
                       IdFerias = 1,
                       IdUsufruto = 1,
                       Status = true,
                       UsufrutoInicial = Convert.ToDateTime("20/10/2021"),
                       UsufrutoFinal = Convert.ToDateTime("30/10/2021")
                   },
                   new()
                   {
                       IdFerias = 1,
                       IdUsufruto = 1,
                       Status = false,
                       UsufrutoInicial = Convert.ToDateTime("20/10/2021"),
                       UsufrutoFinal = Convert.ToDateTime("30/10/2021")
                   }
                    
                }
            };

            var listaFerias = new List<Ferias> { ferias };
            _feriasServiceMoq.Setup(service => service.GetAllFeriasComUsufruto()).ReturnsAsync(listaFerias);

            var result = await _feriasController.GetAllFeriasComUsufruto();

            var okResult = Assert.IsType <OkObjectResult>(result);
        }

        [Fact]
        public async void RetornaListaDePeriodoAquisitivo()
        {
            var periodoAquisitivo = new ConsultaPeriodoAquisitivoDto
            {
                IdFerias = 1,
                Matricula = 5007950,
                PeriodoAquisitivoInicial = Convert.ToDateTime("10/10/2020"),
                PeriodoAquisitivoFinal = Convert.ToDateTime("10/10/2021")
            };

            var listaFerias = new List<ConsultaPeriodoAquisitivoDto> { periodoAquisitivo };
            _feriasServiceMoq.Setup(service => service.GetAllFerias()).ReturnsAsync(listaFerias);

            var result = await _feriasController.GetAllFerias();

            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void RetornaPeriodoAquisitivo()
        {
            var ferias = new ConsultaPeriodoAquisitivoDto
            {
                IdFerias = 1,
                Matricula = 5007950,
                PeriodoAquisitivoInicial = Convert.ToDateTime("10/10/2020"),
                PeriodoAquisitivoFinal = Convert.ToDateTime("10/10/2021")                
            };
 
            _feriasServiceMoq.Setup(service => service.GetPeriodoAquisitivoPorIdAsync(ferias.IdFerias)).ReturnsAsync(ferias);

            var result = await _feriasController.GetPeriodoAquisitivoPorId(ferias.IdFerias);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
