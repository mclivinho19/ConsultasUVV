using System.ComponentModel.DataAnnotations;

namespace ConsultasUVV.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty; // Armazenaremos o hash

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<Consulta>? Consultas { get; set; }
    }
}