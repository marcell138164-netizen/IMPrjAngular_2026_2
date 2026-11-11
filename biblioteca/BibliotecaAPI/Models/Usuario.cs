using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(12)]
        public string rut { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string nombre { get; set; } = string.Empty;

    
        [Required]
        [StringLength(1)]
        public string dv { get; set; } = string.Empty;



        [Required]
        [StringLength(100)]
        public string apellido { get; set; } = string.Empty;

        [StringLength(150)]
        public string? email { get; set; }

        [StringLength(20)]
        public string? celular { get; set; }

        public DateTime fecha_ingreso { get; set; }

        [StringLength(250)]
        public string? direccion { get; set; }
    }
}

