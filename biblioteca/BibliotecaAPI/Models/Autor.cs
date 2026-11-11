using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Models
{
    [Table("autor")]
    public class Autor
  
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? descripcion { get; set; }
    }
}