using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController(IRabbitMqEnvia rabbitMqEnvia) : ControllerBase
    {
        private readonly IRabbitMqEnvia _rabbitMqEnvia = rabbitMqEnvia;
      
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SendMessage([FromBody] Ferias message)
        {
            _rabbitMqEnvia.SendFerias(message);
            return Ok("Mensagem enviada para a fila");
        }
    }
}
