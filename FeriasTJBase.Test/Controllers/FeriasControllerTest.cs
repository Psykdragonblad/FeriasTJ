using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
                Usufrutos = new List<Usufruto> {
                    new Usufruto() {
                        IdFerias = 1,
                        IdUsufruto = 1,
                        Status = true,
                        UsufrutoInicial = Convert.ToDateTime("10/10/2021"),
                        UsufrutoFinal = Convert.ToDateTime("20/10/2021")
                    },
                    new Usufruto() {
                        IdFerias = 1,
                        IdUsufruto = 2,
                        Status = true,
                        UsufrutoInicial = Convert.ToDateTime("21/10/2021"),
                        UsufrutoFinal = Convert.ToDateTime("30/10/2021")
                    }
                }
            };

            var listaFerias = new List<Ferias> { ferias };
            _feriasServiceMoq.Setup(service => service.GetAllFerias()).ReturnsAsync(listaFerias);

            var result = _feriasController.GetAllFerias();

            var okResult = Assert.IsType <OkObjectResult>(result);
        }
    }
}
