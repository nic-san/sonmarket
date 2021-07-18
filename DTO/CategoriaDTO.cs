using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome de categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome de categoria muito grande, tente um nome menor!")]
        [MinLength(2, ErrorMessage = "Nome de categoria muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }
    }
}