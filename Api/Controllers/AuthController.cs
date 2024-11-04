using Application.UserCases.Users.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação de usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        /// <summary>
        /// Construtor do AuthController.
        /// </summary>
        /// <param name="config">Configurações da aplicação.</param>
        /// <param name="mediator">Mediador para envio de comandos.</param>
        public AuthController(IConfiguration config, IMediator mediator)
        {
            _config = config;
            _mediator = mediator;
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="command">Comando contendo as credenciais do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Token JWT se a autenticação for bem-sucedida, caso contrário, mensagem de erro.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            if (response.Sucess)
            {
                var token = GenerateJwtToken();
                return Ok(token);
            }
            return Unauthorized(response.Message);
        }

        /// <summary>
        /// Gera um token JWT.
        /// </summary>
        /// <returns>Token JWT.</returns>
        private string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(signingCredentials: credentials, expires: DateTime.Now.AddHours(1));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
