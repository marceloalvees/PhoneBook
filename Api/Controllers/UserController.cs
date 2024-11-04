using Application.UserCases.Users.Commands.Delete;
using Application.UserCases.Users.Commands.Enabled;
using Application.UserCases.Users.Commands.Save;
using Application.UserCases.Users.Commands.Update;
using Application.UserCases.Users.Queries.Get;
using Application.UserCases.Users.Queries.GetAll;
using Application.UserCases.Users.Queries.GetByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor do UserController.
        /// </summary>
        /// <param name="mediator">Instância do mediator.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Usuário encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = id }, cancellationToken);
            return Ok(user);
        }

        /// <summary>
        /// Obtém um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Usuário encontrado.</returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetByEmailQuery(email), cancellationToken);
            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="command">Comando para salvar o usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return response.Sucess ? Created(string.Empty, null) : BadRequest(response.Message);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="command">Comando para atualizar o usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return response.Sucess ? Ok(response.Message) : BadRequest(response.Message);
        }

        /// <summary>
        /// Deleta um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete([FromRoute] string email, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteUserCommand(email), cancellationToken);
            return response.Sucess ? Ok(response.Message) : NotFound(response.Message);
        }

        /// <summary>
        /// Ativa ou desativa um usuário.
        /// </summary>
        /// <param name="command">Comando para ativar/desativar o usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("enabled")]
        public async Task<IActionResult> Enabled([FromBody] EnabledUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return response.Sucess ? Ok(response.Message) : NotFound(response.Message);
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de usuários.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUsersQuery(), cancellationToken);
            return response.Sucess ? Ok(response.Users) : BadRequest();
        }
    }
}
