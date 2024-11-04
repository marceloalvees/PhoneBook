using Application.Dto;
using Application.UserCases.Agenda.Commands.Save;
using Application.UserCases.Agenda.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar contatos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ContactController"/>.
        /// </summary>
        /// <param name="mediator">Instância do mediador para enviar comandos e consultas.</param>
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Salva uma lista de contatos.
        /// </summary>
        /// <param name="request">Lista de contatos a serem salvos.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Retorna o resultado da operação.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ContactDto> request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SaveContactCommands { Contacts = request }, cancellationToken);
            return response.Sucess ? Created(string.Empty, null) : BadRequest(response.Message);
        }

        /// <summary>
        /// Obtém todos os contatos de um usuário específico.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Retorna a lista de contatos do usuário.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllByUserQueries(id), cancellationToken);
            return Ok(response);
        }
    }
}
