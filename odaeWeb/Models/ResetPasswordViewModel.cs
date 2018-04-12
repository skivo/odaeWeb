using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "La {0} debe tener mínimo de {2} y máximo de {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
