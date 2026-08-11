using System.ComponentModel.DataAnnotations;

namespace RestauranteTDS03.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")] 
        public string Senha { get; set; } = string.Empty;
    }
}
