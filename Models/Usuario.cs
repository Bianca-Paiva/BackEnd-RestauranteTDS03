using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BackEndMobile.Models
{
    [Table("TB_USUARIOS")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;

        public string? ImagemUrl { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        public string? TokenRecuperacao { get; set; }

        public DateTime DataExpiracaoToken { get; set; }

        public bool TokenUsado { get; set; } = false;
    }
}