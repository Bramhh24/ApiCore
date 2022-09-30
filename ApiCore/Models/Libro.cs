using System.ComponentModel.DataAnnotations;

namespace ApiCore.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, ErrorMessage = "El {0} debe ser de al menos {2} y máximo {1} caracteres", MinimumLength = 5)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "La {0} debe ser de al menos {2} y máximo {1} caracteres", MinimumLength = 20)]
        [Display(Name = "Descrición")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Fecha de lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public double Precio { get; set; }
    }
}
