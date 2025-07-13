using System.ComponentModel.DataAnnotations;

namespace P16OWWiki2.Models
{
    public class Heroe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Nombre Real")]
        [StringLength(30, MinimumLength = 2)]
        public string RNombre { get; set; }

        [Required(ErrorMessage = "Requiere edad")]
        public int Edad { get; set; }

        [Display(Name = "Ocupación")]
        public string Ocupa { get; set; }

        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "Requiere almenos una afiliación")]
        [Display(Name = "Primera filiacion")]
        public string Afiliacion1 { get; set; }

        [Display(Name = "Segunda filiacion")]
        public string Afiliacion2 { get; set; }

        [Required]
        public string Rol { get; set; }

        [Display(Name = "Puntos de Salud base")]
        public int Salud { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Requiere imagen")]
        public string Nomfoto { get; set; }

    }
}
