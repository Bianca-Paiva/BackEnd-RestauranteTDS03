using RestauranteTDS03.Models;

namespace RestauranteTDS03.Models.DTOs
{
    public class ResponseDTO
    {
        public bool Erro { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public Usuario? Usuario { get; set; }
    }
}
