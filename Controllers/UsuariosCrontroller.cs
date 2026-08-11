using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteTDS03.Data;
using RestauranteTDS03.Models;
using RestauranteTDS03.Models.DTOs;
using RestauranteTDS03.Models.Response;
using RestauranteTDS03.Service;
using System.Threading.Tasks;

namespace RestauranteTDS03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AuthService _authService;

        public UsuariosController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<Usuario>> CriarUsuario([FromBody] CadastroDTO dadosCadastro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuarios = new Usuario
            {
                Nome = dadosCadastro.Nome,
                NomeUsuario = dadosCadastro.NomeUsuario,
                Email = dadosCadastro.Email,
                Telefone = dadosCadastro.Telefone,
                Senha = (dadosCadastro.Senha),
                ImagemUrl = dadosCadastro.ImagemUrl
            };

            var response = await _authService.CadastrarUsuarioAsync(dadosCadastro);

            // Se o serviço acusar erro (ex: email já existe)
            if (response.Erro)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(CriarUsuario), new { id = usuarios.Id }, new
            {
                Message = "Usuário criado com sucesso.",
                Nome = usuarios.Nome
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO dadosLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Chama o serviço de autenticação para realizar o login
            ResponseLogin response = await _authService.LoginAsync(dadosLogin);

            if (response.Erro)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}