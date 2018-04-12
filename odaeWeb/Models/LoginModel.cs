using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Se debe introducir el Usuario.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Se debe introducir la Contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
