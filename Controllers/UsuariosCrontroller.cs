using API_BackEndMobile.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteTDS03.Data;
using RestauranteTDS03.Models;
using RestauranteTDS03.Models.DTOs;
using RestauranteTDS03.Service;
using System.Threading.Tasks;

namespace RestauranteTDS03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<Usuario>> CriarUsuario(AuthDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuarios = new Usuario
            {
                Nome = dto.Nome,
                NomeUsuario = dto.NomeUsuario,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Senha = (dto.Senha),
                ImagemUrl = dto.ImagemUrl
            };

            await _usuarioService.CriarUsuario(usuarios);

            return CreatedAtAction(nameof(CriarUsuario), new { id = usuarios.Id }, new
            {
                Message = "Usuário criado com sucesso.",
                Nome = usuarios.Nome
            });
        }
    }

}
