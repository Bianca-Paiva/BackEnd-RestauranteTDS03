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
        private readonly AppDbContext _context;

        public UsuariosController(AuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }


        // Endpoint para criar um novo usuário
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


        // Endpoint para login de usuário
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


        // Endpoint para upload de imagem de perfil
        [HttpPost("upload-perfil")]
        public async Task<IActionResult> UploadProfilePicture( IFormFile profilePicture, int? usuarioId)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            return BadRequest(new
            {
                mensagem = "Nenhuma imagem foi enviada."
            });

            var uploadsFolder = Path.Combine( Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName =
                Guid.NewGuid().ToString() + "_" +
                Path.GetFileName(profilePicture.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                uniqueFileName
            );

            using (var stream = new FileStream( filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(stream);
            }

            var caminhoNoBanco = $"/uploads/{uniqueFileName}";

            if (usuarioId.HasValue && usuarioId.Value > 0)
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);

                if (usuario != null)
                {
                    usuario.ImagemUrl = caminhoNoBanco;
                    await _context.SaveChangesAsync();
                }
            }

            return Ok(new
            {
                mensagem = "Upload realizado com sucesso!",
                urlImagem = caminhoNoBanco
            });
        }
    }
}