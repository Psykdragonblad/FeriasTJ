using FeriasTJBase.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJBase.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsufrutoController : ControllerBase
    {
        private readonly IUsufrutoService _usufrutoService;

        public UsufrutoController(IUsufrutoService usufrutoService)
        {
            _usufrutoService = usufrutoService;
        }

        [HttpGet]
        public IActionResult GetAllUsufruto() 
        {
            var listaUsufruto = _usufrutoService.GetAllUsufruto();

            return Ok(listaUsufruto);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsufrutoById(int id)
        {
            var usufruto = _usufrutoService.GetUsufrutoPeloId(id);

            return Ok(usufruto);
        }
    }
}
