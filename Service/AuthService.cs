using RestauranteTDS03.Data;
using RestauranteTDS03.Models.Response;
using RestauranteTDS03.Models.DTOs;
using RestauranteTDS03.Models;
using Microsoft.EntityFrameworkCore;

namespace RestauranteTDS03.Service
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseCadastro> CadastrarUsuarioAsync(CadastroDTO dadosCadastro)
        {
            Usuario? usuarioExistente = await _context.Usuarios.
               FirstOrDefaultAsync(usuario => usuario.Email == dadosCadastro.Email);

            if (usuarioExistente != null)
            {
                return new ResponseCadastro
                {
                    Erro = true,
                    Mensagem = "Este email já está cadastrado no sistema"
                };
            }

            Usuario usuario = new()
            {
                Nome = dadosCadastro.Nome,
                NomeUsuario = dadosCadastro.NomeUsuario,
                Email = dadosCadastro.Email,
                Senha = (dadosCadastro.Senha),
                Telefone = dadosCadastro.Telefone,   
                ImagemUrl = dadosCadastro.ImagemUrl
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new ResponseCadastro
            {
                Erro = false,
                Mensagem = "Usuário cadastrado com sucesso",
                Usuario = usuario
            };
        }

        public async Task<ResponseLogin> LoginAsync(LoginDTO dadosLogin)
        {

            Usuario? usuario = await _context.Usuarios.
               FirstOrDefaultAsync(usuario => usuario.Email == dadosLogin.Email);

            if (usuario == null)
            {
                return new ResponseLogin
                {
                    Erro = true,
                    Mensagem = "Email ou senha inválidos"
                };
            }

            if (usuario.Senha != dadosLogin.Senha)
            {
                return new ResponseLogin
                {
                    Erro = true,
                    Mensagem = "Email ou senha inválidos"
                };
            }

            if (!usuario.Ativo)
            {
                return new ResponseLogin
                {
                    Erro = true,
                    Mensagem = "Usuário inativo. Entre em contato com o suporte"
                };
            }

            return new ResponseLogin
            {
                Erro = false,
                Mensagem = "Login realizado com sucesso",
                Usuario = usuario
            };

        }
    }
}
