using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class CodificadorViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "El Usuario es obligatorio.")]
        [StringLength(5, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 5)]
        [Display(Name = "Usuario")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Se debe introducir el Nombre.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener mínimo de {2} y máximo de {1} caracteres.", MinimumLength = 6)]
        [Display(Name = "Nombre")]
        public string NombreCodificador { get; set; }

        [Required(ErrorMessage = "Se debe introducir el Email.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "La {0} debe tener mínimo de {2} y máximo de {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [StringLength(20, ErrorMessage = "La {0} debe tener mínimo de {2} y máximo de {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
