using FeriasTJBase.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJBase.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeriasController : ControllerBase
    {
        private readonly IFeriasService _feriasService;

        public FeriasController(IFeriasService feriasService)
        {
            _feriasService = feriasService;
        }

        [HttpGet]
        public IActionResult GetAllFerias()
        {
            var listaFerias = _feriasService.GetAllFerias();

            return Ok(listaFerias);
        }

        [HttpGet("{id}")]
        public IActionResult GetPeriodoAquisitivoPorId(int id)
        {
            var listaFerias = _feriasService.GetPeriodoAquisitivoPorIdAsync(id);

            return Ok(listaFerias);
        }
    }
}
