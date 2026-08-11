using System.ComponentModel.DataAnnotations;

namespace RestauranteTDS03.Models.DTOs
{
    public class CadastroDTO 
    {
            [Required(ErrorMessage = "O nome é obrigatório.")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
            public string NomeUsuario { get; set; } = string.Empty;

            [Required(ErrorMessage = "O e-mail é obrigatório.")]
            [EmailAddress(ErrorMessage = "E-mail inválido.")]
            public string Email { get; set; } = string.Empty;

            public string? Telefone { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
            public string Senha { get; set; } = string.Empty;

            [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
            public string ConfirmarSenha { get; set; } = string.Empty;

            public string? ImagemUrl { get; set; }
    }
}
