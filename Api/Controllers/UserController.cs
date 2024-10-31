using Application.UserCases.Users.Commands.Save;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar usuários.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator; 

        /// <summary>
        /// Construtor do UserController.
        /// </summary>
        /// <param name="logger">Instância do logger.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetByEmail([FromRoute] string email)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] SaveUserCommand command)
        {
            return Ok();
        }

    }
}
