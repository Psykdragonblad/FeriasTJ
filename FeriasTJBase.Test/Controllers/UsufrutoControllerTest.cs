using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace FeriasTJBase.Test.Controllers
{
    public class UsufrutoControllerTest
    {
        private readonly Mock<IUsufrutoService> _usufrutoServiceMock;
        private readonly UsufrutoController _usufrutoControllerMock;
        private readonly Mock<ILogger<UsufrutoController>> _loggerMock;

        public UsufrutoControllerTest()
        {
            _usufrutoServiceMock = new Mock<IUsufrutoService>();
            _loggerMock = new Mock<ILogger<UsufrutoController>>();
            _usufrutoControllerMock = new UsufrutoController(_usufrutoServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void RetornaUsufrutoPeloId()
        {
            var usufruto = new Usufruto
            {
                IdFerias = 1,
                IdUsufruto = 1,
                Status = true,
                UsufrutoFinal = Convert.ToDateTime(DateTime.Now),
                UsufrutoInicial = Convert.ToDateTime(DateTime.Now)
            };
            _usufrutoServiceMock.Setup(f => f.GetUsufrutoPeloId(1)).ReturnsAsync(usufruto);

            var resultado = await _usufrutoControllerMock.GetUsufrutoById(1);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async void RetornaListaUsufruto()
        {
            var usufrutos = new List<Usufruto>
            {
                new (){
                    IdFerias = 1,
                    IdUsufruto = 1,
                    Status = true,
                    UsufrutoFinal = Convert.ToDateTime(DateTime.Now),
                    UsufrutoInicial = Convert.ToDateTime(DateTime.Now)
                },
                new (){
                    IdFerias = 1,
                    IdUsufruto = 2,
                    Status = false,
                    UsufrutoFinal = Convert.ToDateTime(DateTime.Now),
                    UsufrutoInicial = Convert.ToDateTime(DateTime.Now)
                },
            };
            _usufrutoServiceMock.Setup(f => f.GetAllUsufruto()).ReturnsAsync(usufrutos);

            var resultado = await _usufrutoControllerMock.GetAllUsufruto();

            Assert.IsType<OkObjectResult>(resultado);
        }
    }
}
