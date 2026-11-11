using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Models
{
    [Table("libro")]
    public class Libro
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string titulo { get; set; } = string.Empty;

        [StringLength(250)]
        public string? editorial { get; set; }

        public int id_genero { get; set; }

        public int id_autor { get; set; }

        public int anio_publicacion { get; set; }
    }
}

        
