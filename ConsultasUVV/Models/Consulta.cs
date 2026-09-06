using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultasUVV.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Especialidade é obrigatória")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Especialidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data/hora é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Descricao { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}