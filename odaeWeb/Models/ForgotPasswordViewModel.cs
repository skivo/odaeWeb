using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb.Models
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Se debe introducir el Usuario o Email.")]
        public string Username { get; set; }

    }
}
