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
        private readonly FeriasController _feriasController;
        private readonly Mock<IFeriasService> _feriasServiceMoq;

        public FeriasControllerTest()
        {
            _feriasServiceMoq = new Mock<IFeriasService>();
            _feriasController = new FeriasController(_feriasServiceMoq.Object);
        }

        [Fact]
        public void RetornaListaDeFerias()
        {
            var ferias = new Ferias
            {
                IdFerias = 1,
                Matricula = 5007950,
                PeriodoAquisitivoInicial = Convert.ToDateTime("10/10/2020"),
                PeriodoAquisitivoFinal = Convert.ToDateTime("10/10/2021"),
                Usufrutos = new List<Usufruto>
                {
                   new Usufruto
                   {
                       IdFerias = 1,
                       IdUsufruto = 1,
                       Status = true,
                       UsufrutoInicial = Convert.ToDateTime("20/10/2021"),
                       UsufrutoFinal = Convert.ToDateTime("30/10/2021")
                   },
                   new Usufruto
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

            var result = _feriasController.GetAllFerias();

            var okResult = Assert.IsType <OkObjectResult>(result);
        }

        [Fact]
        public void RetornaListaDePeriodoAquisitivo()
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

            var result = _feriasController.GetAllFerias();

            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RetornaPeriodoAquisitivo()
        {
            var ferias = new ConsultaPeriodoAquisitivoDto
            {
                IdFerias = 1,
                Matricula = 5007950,
                PeriodoAquisitivoInicial = Convert.ToDateTime("10/10/2020"),
                PeriodoAquisitivoFinal = Convert.ToDateTime("10/10/2021")                
            };
 
            _feriasServiceMoq.Setup(service => service.GetPeriodoAquisitivoPorIdAsync(ferias.IdFerias)).ReturnsAsync(ferias);

            var result = _feriasController.GetPeriodoAquisitivoPorId(ferias.IdFerias);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
