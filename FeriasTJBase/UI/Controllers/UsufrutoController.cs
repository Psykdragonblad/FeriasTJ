using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJBase.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsufrutoController(IUsufrutoService usufrutoService) : ControllerBase
    {
        private readonly IUsufrutoService _usufrutoService = usufrutoService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Usufruto>))]
        public async Task<IActionResult> GetAllUsufruto() 
        {
            var listaUsufruto = await _usufrutoService.GetAllUsufruto();

            return Ok(listaUsufruto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usufruto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsufrutoById(int id)
        {
            var usufruto = await _usufrutoService.GetUsufrutoPeloId(id);

            return Ok(usufruto);
        }
    }
}
